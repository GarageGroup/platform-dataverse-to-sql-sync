using System;
using GGroupp.Infra;

namespace GarageGroup.Platform.DataMover;

[HandlerDataJson("data")]
public sealed record class EventDataJson
{
    public string? EventType { get; init; }

    public string? EntityName { get; init; }

    public Guid EntityId { get; init; }
}