using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public async ValueTask EnsureAuditDateTimeTableAsync(CancellationToken cancellationToken)
        =>
        await sqlExecuteNonQueryApi.ExecuteNonQueryAsync(new DbQuery(DbAuditCreateTableQuery), cancellationToken).ConfigureAwait(false);
}