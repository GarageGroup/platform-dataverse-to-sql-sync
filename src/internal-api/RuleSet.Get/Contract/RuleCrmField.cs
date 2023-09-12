namespace GarageGroup.Platform.DataMover;

public sealed record class RuleCrmField
{
    public RuleCrmField(string name, string? lookupName = null)
    {
        Name = name ?? string.Empty;
        LookupName = lookupName;
    }

    public string Name { get; }

    public string? LookupName { get; }
}