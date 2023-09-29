using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class CrmEntityFlowGetFunc : ICrmEntityFlowGetFunc
{
    private readonly IDataverseEntitySetGetSupplier dataverseApiClient;

    internal CrmEntityFlowGetFunc(IDataverseEntitySetGetSupplier dataverseApiClient)
        =>
        this.dataverseApiClient = dataverseApiClient;
}