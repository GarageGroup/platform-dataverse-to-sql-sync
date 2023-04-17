using System;

namespace GarageGroup.Platform.DataMover;

internal interface IDateTimeOffsetProvider
{
    DateTimeOffset Now { get; }
}