using System;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataMover;

public interface IDataMoveHandler : IHandler<Unit, Unit>
{
}