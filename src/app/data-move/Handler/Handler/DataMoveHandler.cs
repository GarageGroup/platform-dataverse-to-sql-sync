namespace GarageGroup.Platform.DataMover;

internal sealed partial class DataMoveHandler : IDataMoveHandler
{
    private readonly IDataMoveSupplier dataMoverApi;

    internal DataMoveHandler(IDataMoveSupplier dataMoverApi)
        =>
        this.dataMoverApi = dataMoverApi;
}