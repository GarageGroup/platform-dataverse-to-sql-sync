using System;

namespace GarageGroup.Platform.DataMover;

public sealed record class DbDataDeleteIn
{
    public DbDataDeleteIn(string tableName, DateTimeOffset syncTime, Guid? crmId = null)
    {
        TableName = tableName ?? string.Empty;
        SyncTime = syncTime;
        CrmId = crmId;
    }

    public string TableName { get; }

    public DateTimeOffset SyncTime { get; }

    public Guid? CrmId { get; }
}