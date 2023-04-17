using System;

namespace GarageGroup.Platform.DataMover;

public sealed record class RuleSetGetOption
{
    public RuleSetGetOption(string configFilePath, FlatArray<string> entityNames)
    {
        ConfigFilePath = configFilePath.OrEmpty();
        EntityNames = entityNames;
    }

    public string ConfigFilePath { get; }

    public FlatArray<string> EntityNames { get; }
}