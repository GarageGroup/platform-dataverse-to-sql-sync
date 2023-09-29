using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDatabaseDataUpdateSupplier
{
    ValueTask<DbDataUpdateOut> UpdateDataAsync(DbDataUpdateIn input, CancellationToken cancellationToken);
}