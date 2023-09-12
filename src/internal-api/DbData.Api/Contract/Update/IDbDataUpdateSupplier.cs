using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataMover;

public interface IDbDataUpdateSupplier
{
    ValueTask<DbDataUpdateOut> UpdateAsync(DbDataUpdateIn input, CancellationToken cancellationToken);
}