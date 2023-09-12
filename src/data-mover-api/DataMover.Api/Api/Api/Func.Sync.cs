using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Platform.DataMover;

partial class DataMoverApi
{
    public async ValueTask<Unit> SynchronizeAsync(DataSyncIn input, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await GetEntityRulesAsync(input, cancellationToken).ConfigureAwait(false);
            if (entities.IsEmpty)
            {
                logger?.LogWarning("Rules for entity {entityName} were not found", input.CrmEntityName);
                return default;
            }

            logger?.LogInformation("Start synchronization entity {entityName} {entityId}", input.CrmEntityName, input.CrmEntityId);

            foreach (var entityInput in entities.Map(MapRuleEntity))
            {
                await entityMoveFunc.InvokeAsync(entityInput, cancellationToken).ConfigureAwait(false);
            }

            logger?.LogInformation("Entity {entityName} {entityId} has been synchronized", input.CrmEntityName, input.CrmEntityId);
            return default;
        }
        catch (OperationCanceledException ex)
        {
            logger?.LogError(ex, "The operation has been canceled");
            throw;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "An unexpected exception was thrown");
            throw;
        }

        EntityMoveIn MapRuleEntity(RuleEntity entity)
            =>
            new(
                rule: entity,
                crmEntityId: input.CrmEntityId,
                deleteOnly: input.EventType is DataSyncEventType.Delete,
                moveOnly: input.EventType is DataSyncEventType.Create);
    }

    private ValueTask<FlatArray<RuleEntity>> GetEntityRulesAsync(DataSyncIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input, cancellationToken)
        .Pipe(
            static @in => new RuleSetGetIn(
                crmEntityName: @in.CrmEntityName,
                syncOnly: @in.EventType is not DataSyncEventType.Update))
        .PipeValue(
            ruleSetGetFunc.InvokeAsync)
        .Pipe(
            static @out => @out.Entities);
}