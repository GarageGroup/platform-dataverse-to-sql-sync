using System;

namespace GarageGroup.Platform.DataMover;

public readonly record struct RuleSetGetOut
{
    public RuleSetGetOut(FlatArray<RuleEntity> entities)
        =>
        Entities = entities;

    public FlatArray<RuleEntity> Entities { get; }
}