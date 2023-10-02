using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public async ValueTask<Unit> EnsureAuditDateTimeTableAsync(Unit _, CancellationToken cancellationToken)
        =>
        Unit.From(await sqlExecuteNonQueryApi.ExecuteNonQueryAsync(new DbQuery(DbAuditCreateTableQuery), cancellationToken).ConfigureAwait(false));
}