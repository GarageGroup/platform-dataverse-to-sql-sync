using System;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDataSyncHandler : IHandler<EventDataJson, Unit>
{
}