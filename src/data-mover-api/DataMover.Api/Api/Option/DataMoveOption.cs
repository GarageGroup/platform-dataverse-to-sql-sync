namespace GarageGroup.Platform.DataverseToSqlSync;

public readonly record struct DataMoveOption
{
    public int MaxDbBatchCount { get; init; }
}