using System.Text.Json;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

internal static partial class CrmEntityFlowGetFuncSource
{
    internal static JsonElement GetJsonElement<T>(T value)
        =>
        JsonDocument.Parse(JsonSerializer.Serialize(value)).RootElement;
}