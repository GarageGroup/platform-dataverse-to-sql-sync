namespace GarageGroup.Platform.DataMover;

public readonly record struct DbDataUpdateOut
{
    public DbDataUpdateOut(int synchronizedRows)
        =>
        SynchronizedRows = synchronizedRows;

    public int SynchronizedRows { get; }
}