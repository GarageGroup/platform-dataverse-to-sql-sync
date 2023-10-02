using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IAuditDateTimeTableEnsureSupplier
{
    ValueTask<Unit> EnsureAuditDateTimeTableAsync(Unit _, CancellationToken cancellationToken);
}