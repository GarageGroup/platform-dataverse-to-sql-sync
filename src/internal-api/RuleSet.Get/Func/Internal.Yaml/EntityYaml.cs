namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed record class EntityYaml
{
    public string? Name { get; init; }

    public string? Plural { get; init; }

    public string? Filter { get; init; }

    public string? Annotations { get; init; }

    public TableYaml[]? Tables { get; init; }
}