namespace GarageGroup.Platform.DataMover;

public sealed record class CrmEntityLookup
{
    public CrmEntityLookup(string rootFieldName, string lookupFieldName)
    {
        RootFieldName = rootFieldName ?? string.Empty;
        LookupFieldName = lookupFieldName ?? string.Empty;
    }

    public string RootFieldName { get; }

    public string LookupFieldName { get; }
}