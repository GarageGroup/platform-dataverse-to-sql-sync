using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public ValueTask<Unit> EnsureAuditDateTimeTableAsync(Unit _, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            DbAuditCreateTableQuery, cancellationToken)
        .PipeValue(
            sqlApi.ExecuteNonQueryAsync)
        .Pipe(
            Unit.From);
}