using System;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataMover;

public static class DataMoveDependency
{
    public static Dependency<IDataSyncHandler> UseDataSyncHandler<TDataMoverApi>(this Dependency<TDataMoverApi> dependency)
        where TDataMoverApi : IDataSyncSupplier
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IDataSyncHandler>(InnerCreateHandler);

        static DataSyncHandler InnerCreateHandler(TDataMoverApi dataMoverApi)
        {
            ArgumentNullException.ThrowIfNull(dataMoverApi);
            return new(dataMoverApi);
        }
    }
}