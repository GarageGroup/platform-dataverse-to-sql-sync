namespace GarageGroup.Platform.DataMover;

internal readonly record struct ConfigurationYaml
{
    public EntityYaml[]? Entities { get; init; }
}