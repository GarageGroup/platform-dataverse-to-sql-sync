using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IRuleSetGetFunc
{
    ValueTask<RuleSetGetOut> InvokeAsync(RuleSetGetIn input, CancellationToken cancellationToken);
}