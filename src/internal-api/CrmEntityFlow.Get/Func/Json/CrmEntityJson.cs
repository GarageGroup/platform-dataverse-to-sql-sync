using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarageGroup.Platform.DataMover;

internal sealed record class CrmEntityJson
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; init; }
}