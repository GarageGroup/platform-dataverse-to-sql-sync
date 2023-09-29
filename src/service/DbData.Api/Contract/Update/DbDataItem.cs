using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class DbDataItem
{
    public DbDataItem(Guid crmId, FlatArray<DbDataFieldValue> fieldValues)
    {
        CrmId = crmId;
        FieldValues = fieldValues;
    }

    public Guid CrmId { get; }

    public FlatArray<DbDataFieldValue> FieldValues { get; }
}