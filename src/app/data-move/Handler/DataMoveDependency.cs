using System;
using GarageGroup.Infra;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

public static class DataMoveDependency
{
    public static Dependency<IHandler<Unit, Unit>> UseDataMoveHandler<TDataMoverApi>(
        this Dependency<TDataMoverApi> dependency)
        where TDataMoverApi : IDataMoveSupplier
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IHandler<Unit, Unit>>(InnerCreateHandler);

        static DataMoveHandler InnerCreateHandler(TDataMoverApi dataMoverApi)
        {
            ArgumentNullException.ThrowIfNull(dataMoverApi);
            return new(dataMoverApi);
        }
    }
}