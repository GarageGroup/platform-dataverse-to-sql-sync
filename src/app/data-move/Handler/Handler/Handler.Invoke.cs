using System;
using System.Threading;
using System.Threading.Tasks;
using GGroupp.Infra;

namespace GarageGroup.Platform.DataMover;

partial class DataMoveHandler
{
    public ValueTask<Result<Unit, HandlerFailure>> HandleAsync(Unit input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input, cancellationToken)
        .HandleCancellation()
        .PipeValue(
            dataMoverApi.MoveAsync)
        .Pipe(
            static success => Result.Success(success).With<HandlerFailure>());
}