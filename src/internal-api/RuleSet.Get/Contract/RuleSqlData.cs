namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class RuleSqlData
{
    public RuleSqlData(string tableName, string keyFieldName)
    {
        TableName = tableName ?? string.Empty;
        KeyFieldName = keyFieldName ?? string.Empty;
    }

    public string TableName { get; }

    public string KeyFieldName { get; }
}