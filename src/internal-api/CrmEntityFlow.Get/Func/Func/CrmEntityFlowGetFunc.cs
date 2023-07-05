using GarageGroup.Infra;

namespace GarageGroup.Platform.DataMover;

internal sealed partial class CrmEntityFlowGetFunc : ICrmEntityFlowGetFunc
{
    private readonly IDataverseEntitySetGetSupplier dataverseApiClient;

    internal CrmEntityFlowGetFunc(IDataverseEntitySetGetSupplier dataverseApiClient)
        =>
        this.dataverseApiClient = dataverseApiClient;
}