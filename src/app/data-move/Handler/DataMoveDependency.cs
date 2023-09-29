using System;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

public static class DataMoveDependency
{
    public static Dependency<IDataMoveHandler> UseDataMoveHandler<TDataMoverApi>(this Dependency<TDataMoverApi> dependency)
        where TDataMoverApi : IDataMoveSupplier
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IDataMoveHandler>(InnerCreateHandler);

        static DataMoveHandler InnerCreateHandler(TDataMoverApi dataMoverApi)
        {
            ArgumentNullException.ThrowIfNull(dataMoverApi);
            return new(dataMoverApi);
        }
    }
}