using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IAuditDateTimeTableWriteSupplier
{
    ValueTask<Result<Unit, Failure<Unit>>> WriteAuditDateTimeAsync(AuditDateTimeWriteIn input, CancellationToken cancellationToken);
}