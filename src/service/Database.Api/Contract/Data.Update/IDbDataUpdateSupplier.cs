using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDbDataUpdateSupplier
{
    ValueTask<DbDataUpdateOut> UpdateDataAsync(DbDataUpdateIn input, CancellationToken cancellationToken);
}