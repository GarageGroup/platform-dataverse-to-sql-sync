using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public readonly record struct RuleSetGetOut
{
    public RuleSetGetOut(FlatArray<RuleEntity> entities)
        =>
        Entities = entities;

    public FlatArray<RuleEntity> Entities { get; }
}