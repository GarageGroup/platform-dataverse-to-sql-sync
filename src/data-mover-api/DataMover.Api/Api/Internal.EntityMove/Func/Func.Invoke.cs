using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Platform.DataMover;

partial class EntityMoveFunc
{
    public async ValueTask<Unit> InvokeAsync(EntityMoveIn input, CancellationToken cancellationToken)
    {
        if (input.Rule.Tables.IsEmpty)
        {
            return default;
        }

        var syncTime = DateTimeOffset.Now;
        var result = await MoveEntityAsync(input, cancellationToken).ConfigureAwait(false);

        if (input.MoveOnly)
        {
            return default;
        }

        foreach (var table in result.TableNames)
        {
            var dbDeleteIn = new DbDataDeleteIn(table, syncTime, input.CrmEntityId);
            await DeleteAsync(dbDeleteIn, cancellationToken).ConfigureAwait(false);
        }

        return default;
    }

    private async ValueTask<MoveEntityResult> MoveEntityAsync(EntityMoveIn input, CancellationToken token)
    {
        var syncTables = input.Rule.Tables.Filter(IsSyncOperation).Map(GetTableName);
        if (input.DeleteOnly)
        {
            return new(syncTables);
        }

        var flowInput = new CrmEntityFlowGetIn(
            entityName: input.Rule.CrmData.EntityName,
            pluralName: input.Rule.CrmData.EntityPluralName,
            fields: input.Rule.CrmData.Fields.Filter(IsNotLookup).Map(GetCrmFieldName),
            lookups: input.Rule.CrmData.Fields.Filter(IsLookup).Map(GetLookup),
            filter: BuildCrmFilter(input.Rule.CrmData, input.CrmEntityId),
            pageSize: GetCrmPageSize(input.Rule.CrmData.Fields.Length),
            includeAnnotations: input.Rule.CrmData.IncludeAnnotations);

        var resultTables = syncTables.ToList();

        await foreach (var crmEntitySet in flowGetFunc.InvokeAsync(flowInput, token).ConfigureAwait(false))
        {
            foreach (var table in input.Rule.Tables)
            {
                var moved = await MoveEntitySetAsync(crmEntitySet, table, token).ConfigureAwait(false);
                if (moved < crmEntitySet.Entities.Length || input.CrmEntityId is null || resultTables.Contains(table.TableName) is false)
                {
                    continue;
                }

                resultTables.Remove(table.TableName);
            }
        }

        return new(resultTables);

        static bool IsNotLookup(RuleCrmField field)
            =>
            string.IsNullOrEmpty(field.LookupName);

        static bool IsLookup(RuleCrmField field)
            =>
            string.IsNullOrEmpty(field.LookupName) is false;

        static string GetCrmFieldName(RuleCrmField field)
            =>
            field.Name.Split('@')[0];

        static CrmEntityLookup GetLookup(RuleCrmField field)
            =>
            new(field.Name, field.LookupName.OrEmpty());

        static string GetTableName(RuleTable table)
            =>
            table.TableName;

        static bool IsSyncOperation(RuleTable table)
            =>
            table.Operation is RuleItemOperation.Sync;
    }

    private async ValueTask<int> MoveEntitySetAsync(CrmEntitySet entitySet, RuleTable rule, CancellationToken token)
    {
        if (entitySet.Entities.IsEmpty)
        {
            return default;
        }

        var skippedFields = rule.Fields.AsEnumerable().Where(SkipNullable).Select(GetSqlFieldName).ToArray();
        var isJoinOperation = rule.Operation is RuleItemOperation.Join;

        var batchSize = GetBatchSize(rule.Fields.Length);
        var dbDataItems = new List<DbDataItem>(batchSize);

        var batchCount = GetMaxDbBatchCount();
        var dbTasks = new List<Task<int>>(batchCount);

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var movedEntities = 0;

        foreach (var crmEntity in entitySet.Entities)
        {
            var dbDataItem = GetDbDataItem(crmEntity, rule.Fields);
            if (dbDataItem.FieldValues.FirstOrAbsent(CheckIfSkipValue).OnPresent(LogSkipped).IsPresent)
            {
                continue;
            }

            dbDataItems.Add(dbDataItem);
            if ((dbDataItems.Count < dbDataItems.Capacity) && IsNotTimerExceeded(stopWatch))
            {
                continue;
            }

            var dbTask = UpdateAsync(dbDataItems);
            dbDataItems.Clear();

            dbTasks.Add(dbTask);
            if ((dbTasks.Count < dbTasks.Capacity) && IsNotTimerExceeded(stopWatch))
            {
                continue;
            }

            movedEntities += await InvokeAllAsync(dbTasks).ConfigureAwait(false);

            dbTasks.Clear();
            stopWatch.Restart();
        }

        if (dbDataItems.Count > 0)
        {
            var dbTask = UpdateAsync(dbDataItems);
            dbTasks.Add(dbTask);
        }

        return movedEntities + await InvokeAllAsync(dbTasks).ConfigureAwait(false);

        bool CheckIfSkipValue(DbDataFieldValue fieldValue)
            =>
            fieldValue.Value is null && skippedFields.Contains(fieldValue.Name, StringComparer.InvariantCultureIgnoreCase);

        void LogSkipped(DbDataFieldValue fieldValue)
            =>
            logger?.LogWarning("Skip due to field '{skippedField}' value is null", fieldValue.Name);

        async Task<int> UpdateAsync(FlatArray<DbDataItem> items)
        {
            var updateInput = new DbDataUpdateIn(
                tableName: rule.TableName,
                keyFieldName: rule.KeyFieldName,
                items: items,
                type: isJoinOperation ? DbDataUpdateType.UpdateOnly : DbDataUpdateType.CreateOrUpdate,
                syncTime: isJoinOperation is false);

            logger?.LogInformation("Update database items: {count}", items.Length);

            var updateOutput = await dbDataApi.UpdateAsync(updateInput, token).ConfigureAwait(false);
            var affectedRows = updateOutput.SynchronizedRows;

            logger?.LogInformation("#Thread: {threadId}; Affected rows: {count}", Environment.CurrentManagedThreadId, affectedRows);
            return affectedRows;
        }

        static bool SkipNullable(RuleField rule)
            =>
            rule.SkipNullable;

        static string GetSqlFieldName(RuleField field)
            =>
            field.SqlName;
    }

    private async Task DeleteAsync(DbDataDeleteIn input, CancellationToken cancellationToken)
    {
        logger?.LogInformation("Delete entities from the table: {table}. SyncTime: {syncTime}", input.TableName, input.SyncTime);

        var count = await dbDataApi.DeleteAsync(input, cancellationToken).ConfigureAwait(false);

        logger?.LogInformation("Deleted: {count}", count.DeletedRows);
    }

    private static string? BuildCrmFilter(RuleCrmData crmData, Guid? entityId)
    {
        if (entityId is null)
        {
            return crmData.Filter;
        }

        var builder = new StringBuilder(crmData.EntityKeyFieldName).Append(" eq ").Append(entityId);

        if (string.IsNullOrWhiteSpace(crmData.Filter))
        {
            return builder.ToString();
        }

        return builder.Append(" and (").Append(crmData.Filter).Append(')').ToString();
    }

    private static DbDataItem GetDbDataItem(CrmEntity crmEntity, FlatArray<RuleField> fields)
    {
        return new(
            crmId: crmEntity.Id,
            fieldValues: fields.Map(InnerGetFieldValue));

        DbDataFieldValue InnerGetFieldValue(RuleField ruleField)
            =>
            GetFieldValue(crmEntity, ruleField);
    }
}