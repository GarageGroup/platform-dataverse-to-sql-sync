using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataMover;

internal interface IEntityMoveFunc
{
    ValueTask<Unit> InvokeAsync(EntityMoveIn input, CancellationToken cancellationToken);
}