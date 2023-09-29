namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class DataSyncHandler : IDataSyncHandler
{
    private readonly IDataSyncSupplier dataMoverApi;

    internal DataSyncHandler(IDataSyncSupplier dataMoverApi)
        =>
        this.dataMoverApi = dataMoverApi;
}