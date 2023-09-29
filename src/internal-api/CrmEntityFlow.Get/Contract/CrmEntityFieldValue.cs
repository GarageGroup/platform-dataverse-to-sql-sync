using System.Text.Json;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class CrmEntityFieldValue
{
    public CrmEntityFieldValue(string name, JsonElement jsonValue)
    {
        Name = name ?? string.Empty;
        JsonValue = jsonValue;
    }

    public string Name { get; }

    public JsonElement JsonValue { get; }
}