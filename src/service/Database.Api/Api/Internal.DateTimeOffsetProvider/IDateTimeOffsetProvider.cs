using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal interface IDateTimeOffsetProvider
{
    DateTimeOffset Now { get; }
}