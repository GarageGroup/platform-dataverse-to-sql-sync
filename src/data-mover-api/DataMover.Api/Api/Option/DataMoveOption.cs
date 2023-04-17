namespace GarageGroup.Platform.DataMover;

public readonly record struct DataMoveOption
{
    public int MaxDbBatchCount { get; init; }
}