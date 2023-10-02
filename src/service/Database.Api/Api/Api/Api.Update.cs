using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public ValueTask<DbDataUpdateOut> UpdateDataAsync(DbDataUpdateIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input ?? throw new ArgumentNullException(nameof(input)), cancellationToken)
        .HandleCancellation()
        .Pipe(
            CreateDbQuery)
        .PipeValue(
            ExecuteNonQueryAsync)
        .Pipe(
            static affectedRows => new DbDataUpdateOut(affectedRows));

    private ValueTask<int> ExecuteNonQueryAsync(DbCombinedQuery dbQuery, CancellationToken cancellationToken)
        =>
        dbQuery.Queries.IsEmpty ? default : sqlExecuteNonQueryApi.ExecuteNonQueryAsync(dbQuery, cancellationToken);

    private DbCombinedQuery CreateDbQuery(DbDataUpdateIn input)
    {
        return new(input.Items.Map(CreateDbQuery))
        {
            TimeoutInSeconds = DefaultTimeoutInSeconds
        };

        IDbQuery CreateDbQuery(DbDataItem item, int index)
            =>
            CreateDbItemQuery(input, item, index);
    }

    private IDbQuery CreateDbItemQuery(DbDataUpdateIn input, DbDataItem item, int index)
    {
        var fieldValues = item.FieldValues.AsEnumerable().Where(IsNotKeyField).Select(MapFieldValue).ToList();
        if (input.SyncTime && fieldValues.Any(IsSyncTimeField) is false)
        {
            fieldValues.Add(
                new(SyncTimeFieldName, dateTimeOffsetProvider.Now, $"{SyncTimeFieldName}{index}"));
        }

        var dbItemUpdateQuery = new DbUpdateQuery(
            tableName: input.TableName,
            fieldValues: fieldValues,
            filter: new DbParameterFilter(input.KeyFieldName, DbFilterOperator.Equal, item.CrmId, $"{input.KeyFieldName}{index}"));

        if (input.Type is DbDataUpdateType.UpdateOnly)
        {
            return dbItemUpdateQuery;
        }

        var dbItemExistsFilter = new DbExistsFilter(
            selectQuery: new(input.TableName)
            {
                SelectedFields = new(input.KeyFieldName),
                Filter = dbItemUpdateQuery.Filter
            });

        var dbItemInsertQuery = new DbInsertQuery(
            tableName: input.TableName,
            fieldValues: item.FieldValues.Map(MapFieldValue));

        return new DbIfQuery(
            condition: dbItemExistsFilter,
            thenQuery: dbItemUpdateQuery,
            elseQuery: dbItemInsertQuery);

        bool IsNotKeyField(DataFieldValue field)
            =>
            string.Equals(field.Name, input.KeyFieldName, StringComparison.InvariantCulture) is false;

        DbFieldValue MapFieldValue(DataFieldValue field)
            =>
            new(field.Name, field.Value, $"{field.Name}{index}");

        static bool IsSyncTimeField(DbFieldValue field)
            =>
            string.Equals(field.FieldName, SyncTimeFieldName, StringComparison.InvariantCulture);
    }
}