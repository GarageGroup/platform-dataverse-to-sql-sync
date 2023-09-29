using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DataMoverApi
{
    public async ValueTask<Unit> MoveAsync(Unit input, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await GetEntityRulesAsync(input, cancellationToken).ConfigureAwait(false);
            if (entities.IsEmpty)
            {
                logger?.LogWarning("Rules are absent");
                return default;
            }

            logger?.LogInformation("Start data moving");

            foreach (var entityInput in entities.Map(MapRuleEntity))
            {
                await entityMoveFunc.InvokeAsync(entityInput, cancellationToken).ConfigureAwait(false);
            }

            logger?.LogInformation("All data have been moved");
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

        static EntityMoveIn MapRuleEntity(RuleEntity entity)
            =>
            new(
                rule: entity,
                crmEntityId: null,
                deleteOnly: false,
                moveOnly: false);
    }

    private ValueTask<FlatArray<RuleEntity>> GetEntityRulesAsync(Unit input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input, cancellationToken)
        .Pipe(
            static _ => new RuleSetGetIn(crmEntityName: null, syncOnly: true))
        .PipeValue(
            ruleSetGetFunc.InvokeAsync)
        .Pipe(
            static @out => @out.Entities);
}