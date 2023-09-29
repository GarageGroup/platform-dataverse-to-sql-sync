using System;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

public interface IDataMoveHandler : IHandler<Unit, Unit>
{
}