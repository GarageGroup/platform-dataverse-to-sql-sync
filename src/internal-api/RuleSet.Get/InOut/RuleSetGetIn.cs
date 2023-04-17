namespace GarageGroup.Platform.DataMover;

public readonly record struct RuleSetGetIn
{
    public RuleSetGetIn(string? crmEntityName, bool syncOnly)
    {
        CrmEntityName = crmEntityName;
        SyncOnly = syncOnly;
    }

    public string? CrmEntityName { get; }

    public bool SyncOnly { get; }
}