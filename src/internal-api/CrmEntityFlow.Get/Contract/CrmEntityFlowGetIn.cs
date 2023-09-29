using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class CrmEntityFlowGetIn
{
    public CrmEntityFlowGetIn(
        string entityName,
        string pluralName,
        FlatArray<string> fields,
        FlatArray<CrmEntityLookup> lookups,
        string? filter,
        int pageSize,
        string? includeAnnotations)
    {
        EntityName = entityName ?? string.Empty;
        PluralName = pluralName ?? string.Empty;
        Fields = fields;
        Lookups = lookups;
        Filter = filter;
        PageSize = pageSize;
        IncludeAnnotations = includeAnnotations;
    }

    public string EntityName { get; }

    public string PluralName { get; }

    public FlatArray<string> Fields { get; }

    public FlatArray<CrmEntityLookup> Lookups { get; }

    public string? Filter { get; }

    public int PageSize { get; }

    public string? IncludeAnnotations { get; }
}