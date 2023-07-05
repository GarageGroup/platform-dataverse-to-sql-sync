using System;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataMover;

public interface IDataSyncHandler : IHandler<EventDataJson, Unit>
{
}