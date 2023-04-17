using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataMover;

public interface IDataMoveSupplier
{
    ValueTask<Unit> MoveAsync(Unit input, CancellationToken cancellationToken);
}