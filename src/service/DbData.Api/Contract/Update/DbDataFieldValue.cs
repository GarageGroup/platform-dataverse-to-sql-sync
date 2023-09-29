namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class DbDataFieldValue
{
    public DbDataFieldValue(string name, object? value)
    {
        Name = name ?? string.Empty;
        Value = value;
    }

    public string Name { get; }

    public object? Value { get; }
}