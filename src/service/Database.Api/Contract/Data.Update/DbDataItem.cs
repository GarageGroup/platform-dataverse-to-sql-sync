using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class DbDataItem
{
    public DbDataItem(Guid crmId, FlatArray<DataFieldValue> fieldValues)
    {
        CrmId = crmId;
        FieldValues = fieldValues;
    }

    public Guid CrmId { get; }

    public FlatArray<DataFieldValue> FieldValues { get; }
}