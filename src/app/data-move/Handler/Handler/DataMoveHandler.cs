namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class DataMoveHandler : IDataMoveHandler
{
    private readonly IDataMoveSupplier dataMoverApi;

    internal DataMoveHandler(IDataMoveSupplier dataMoverApi)
        =>
        this.dataMoverApi = dataMoverApi;
}