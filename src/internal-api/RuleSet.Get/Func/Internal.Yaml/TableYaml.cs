namespace GarageGroup.Platform.DataMover;

internal sealed record class TableYaml
{
    public string? Name { get; init; }

    public string? Key { get; init; }

    public FieldYaml[]? Fields { get; init; }

    public string? Operation { get; init; }
}