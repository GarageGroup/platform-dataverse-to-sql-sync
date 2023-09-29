using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DataSyncHandler
{
    public ValueTask<Result<Unit, Failure<HandlerFailureCode>>> HandleAsync(
        EventDataJson? handlerData, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            handlerData ?? new(), cancellationToken)
        .HandleCancellation()
        .Pipe(
            static data => new DataSyncIn(
                eventType: ParseEventType(data.EventType),
                crmEntityName: data.EntityName.OrEmpty(),
                crmEntityId: data.EntityId))
        .PipeValue(
            dataMoverApi.SynchronizeAsync)
        .Pipe(
            static @out => Result.Success(@out).With<Failure<HandlerFailureCode>>());

    private static DataSyncEventType ParseEventType(string? eventType)
    {
        if (string.IsNullOrWhiteSpace(eventType))
        {
            return default;
        }

        if (Enum.TryParse<DataSyncEventType>(eventType, true, out var type))
        {
            return type;
        }

        return default;
    }
}