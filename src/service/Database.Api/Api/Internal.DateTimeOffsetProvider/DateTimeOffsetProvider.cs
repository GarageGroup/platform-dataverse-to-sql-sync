using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public static readonly DateTimeOffsetProvider Instance = new();

    private DateTimeOffsetProvider()
    {
    }

    public DateTimeOffset Now => DateTimeOffset.Now;
}