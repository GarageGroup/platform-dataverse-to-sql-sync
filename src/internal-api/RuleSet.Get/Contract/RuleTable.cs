using System;

namespace GarageGroup.Platform.DataMover;

public sealed record class RuleTable
{
    public RuleTable(RuleItemOperation operation, string tableName, string keyFieldName, FlatArray<RuleField> fields)
    {
        Operation = operation;
        TableName = tableName ?? string.Empty;
        KeyFieldName = keyFieldName;
        Fields = fields;
    }

    public RuleItemOperation Operation { get; }

    public string TableName { get; }

    public string KeyFieldName { get; }

    public FlatArray<RuleField> Fields { get; }
}