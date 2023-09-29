using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class DataSyncIn
{
    public DataSyncIn(DataSyncEventType eventType, string crmEntityName, Guid crmEntityId)
    {
        EventType = eventType;
        CrmEntityName = crmEntityName ?? string.Empty;
        CrmEntityId = crmEntityId;
    }

    public DataSyncEventType EventType { get; }

    public string CrmEntityName { get; }

    public Guid CrmEntityId { get; }
}