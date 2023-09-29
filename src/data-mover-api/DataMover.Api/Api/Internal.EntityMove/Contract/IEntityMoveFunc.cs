using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal interface IEntityMoveFunc
{
    ValueTask<Unit> InvokeAsync(EntityMoveIn input, CancellationToken cancellationToken);
}