using System.Collections.Generic;
using System.Threading;

namespace GarageGroup.Platform.DataMover;

public interface ICrmEntityFlowGetFunc
{
    IAsyncEnumerable<CrmEntitySet> InvokeAsync(CrmEntityFlowGetIn input, CancellationToken cancellationToken);
}