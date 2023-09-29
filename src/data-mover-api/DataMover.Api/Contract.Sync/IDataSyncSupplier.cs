using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDataSyncSupplier
{
    ValueTask<Unit> SynchronizeAsync(DataSyncIn input, CancellationToken cancellationToken);
}