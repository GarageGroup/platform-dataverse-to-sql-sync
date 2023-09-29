namespace GarageGroup.Platform.DataverseToSqlSync;

public readonly record struct DbDataDeleteOut
{
    public DbDataDeleteOut(int deletedRows)
        =>
        DeletedRows = deletedRows;

    public int DeletedRows { get; }
}