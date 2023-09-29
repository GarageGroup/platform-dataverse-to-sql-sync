using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public readonly record struct CrmEntitySet
{
    public CrmEntitySet(FlatArray<CrmEntity> entities)
        =>
        Entities = entities;

    public FlatArray<CrmEntity> Entities { get; }
}