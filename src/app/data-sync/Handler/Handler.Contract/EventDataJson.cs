using System;
using System.Text.Json.Serialization;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class EventDataJson
{
    [JsonPropertyName("MessageName")]
    public string? EventType { get; init; }

    [JsonPropertyName("PrimaryEntityName")]
    public string? EntityName { get; init; }

    [JsonPropertyName("PrimaryEntityId")]
    public Guid EntityId { get; init; }
}