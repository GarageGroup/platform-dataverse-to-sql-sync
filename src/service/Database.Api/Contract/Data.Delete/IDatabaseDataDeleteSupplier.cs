using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDatabaseDataDeleteSupplier
{
    ValueTask<DbDataDeleteOut> DeleteDataAsync(DbDataDeleteIn input, CancellationToken cancellationToken);
}