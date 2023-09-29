using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class RuleCrmData
{
    public RuleCrmData(
        string entityName,
        string entityPluralName,
        string entityKeyFieldName,
        FlatArray<RuleCrmField> fields,
        string? filter,
        string? includeAnnotations)
    {
        EntityName = entityName ?? string.Empty;
        EntityPluralName = entityPluralName ?? string.Empty;
        EntityKeyFieldName = entityKeyFieldName ?? string.Empty;
        Fields = fields;
        Filter = filter;
        IncludeAnnotations = includeAnnotations;
    }

    public string EntityName { get; }

    public string EntityPluralName { get; }

    public string EntityKeyFieldName { get; }

    public FlatArray<RuleCrmField> Fields { get; }

    public string? Filter { get; }

    public string? IncludeAnnotations { get; }
}