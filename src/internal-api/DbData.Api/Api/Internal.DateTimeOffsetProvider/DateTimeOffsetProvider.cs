using System;

namespace GarageGroup.Platform.DataMover;

internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public static readonly DateTimeOffsetProvider Instance = new();

    private DateTimeOffsetProvider()
    {
    }

    public DateTimeOffset Now => DateTimeOffset.Now;
}