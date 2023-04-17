using System.Collections.Generic;

namespace GarageGroup.Platform.DataMover;

internal sealed record class FieldYaml
{
    public string? Sql { get; init; }

    public string? Crm { get; init; }

    public string? LookupName { get; init; }

    public string? Type { get; init; }

    public bool SkipNullable { get; init; }

    public Dictionary<string, string?>? Map { get; init; }
}