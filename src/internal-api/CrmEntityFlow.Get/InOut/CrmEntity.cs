using System;

namespace GarageGroup.Platform.DataMover;

public sealed record class CrmEntity
{
    public CrmEntity(Guid id, FlatArray<CrmEntityFieldValue> fieldValues)
    {
        Id = id;
        FieldValues = fieldValues;
    }

    public Guid Id { get; }

    public FlatArray<CrmEntityFieldValue> FieldValues { get; }
}