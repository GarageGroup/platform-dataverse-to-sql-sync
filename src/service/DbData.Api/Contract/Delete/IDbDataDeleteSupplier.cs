using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDbDataDeleteSupplier
{
    ValueTask<DbDataDeleteOut> DeleteAsync(DbDataDeleteIn input, CancellationToken cancellationToken);
}