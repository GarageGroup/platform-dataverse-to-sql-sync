using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IAuditDateTimeTableWriteSupplier
{
    ValueTask<Result<Unit, Failure<WriteAuditDateTimeFailureCode>>> WriteAuditDateTimeAsync(WriteAuditDateTimeIn input, CancellationToken cancellationToken);
}