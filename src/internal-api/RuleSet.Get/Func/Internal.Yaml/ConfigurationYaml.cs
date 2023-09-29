namespace GarageGroup.Platform.DataverseToSqlSync;

internal readonly record struct ConfigurationYaml
{
    public EntityYaml[]? Entities { get; init; }
}