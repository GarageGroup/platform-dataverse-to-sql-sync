using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DbDataApi
{
    public ValueTask<DbDataDeleteOut> DeleteAsync(DbDataDeleteIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input ?? throw new ArgumentNullException(nameof(input)), cancellationToken)
        .HandleCancellation()
        .Pipe(
            static @in => new DbDeleteQuery(
                tableName: @in.TableName,
                filter: CreateDbFilter(@in))
            {
                TimeoutInSeconds = DefaultTimeoutInSeconds
            })
        .PipeValue(
            sqlApi.ExecuteNonQueryAsync)
        .Pipe(
            static affectedRows => new DbDataDeleteOut(affectedRows));

    private static IDbFilter CreateDbFilter(DbDataDeleteIn input)
    {
        var syncTimeFilter = new DbParameterFilter(SyncTimeFieldName, DbFilterOperator.Less, input.SyncTime);
        if (input.CrmId is null)
        {
            return syncTimeFilter;
        }

        var crmIdFilter = new DbParameterFilter(PrimaryKeyFieldName, DbFilterOperator.Equal, input.CrmId.Value);
        return new DbCombinedFilter(DbLogicalOperator.And, new(crmIdFilter, syncTimeFilter));
    }
}