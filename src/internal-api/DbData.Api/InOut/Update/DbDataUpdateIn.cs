using System;

namespace GarageGroup.Platform.DataMover;

public sealed record class DbDataUpdateIn
{
    public DbDataUpdateIn(string tableName, string keyFieldName, FlatArray<DbDataItem> items, DbDataUpdateType type, bool syncTime)
    {
        TableName = tableName ?? string.Empty;
        KeyFieldName = keyFieldName ?? string.Empty;
        Items = items;
        Type = type;
        SyncTime = syncTime;
    }

    public string TableName { get; }

    public string KeyFieldName { get; }

    public FlatArray<DbDataItem> Items { get; }

    public DbDataUpdateType Type { get; }

    public bool SyncTime { get; }
}