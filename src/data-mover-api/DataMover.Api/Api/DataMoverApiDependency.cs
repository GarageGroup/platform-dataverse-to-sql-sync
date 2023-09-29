using System;
using Microsoft.Extensions.Logging;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

public static class DataMoverApiDependency
{
    public static Dependency<IDataMoverApi> UseDataMoverApi(
        this Dependency<ICrmEntityFlowGetFunc, IDatabaseApi, IRuleSetGetFunc, DataMoveOption> dependency)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Fold<IDataMoverApi>(CreateDataMoverApi);
    }

    private static DataMoverApi CreateDataMoverApi(
        IServiceProvider serviceProvider,
        ICrmEntityFlowGetFunc entityFlowGetFunc,
        IDatabaseApi databaseApi,
        IRuleSetGetFunc ruleSetGetFunc,
        DataMoveOption option)
    {
        ArgumentNullException.ThrowIfNull(entityFlowGetFunc);
        ArgumentNullException.ThrowIfNull(databaseApi);
        ArgumentNullException.ThrowIfNull(ruleSetGetFunc);

        var loggerFactory = serviceProvider.GetServiceOrAbsent<ILoggerFactory>().OrDefault();

        return new(
            ruleSetGetFunc: ruleSetGetFunc,
            entityMoveFunc: new EntityMoveFunc(
                flowGetFunc: entityFlowGetFunc,
                databaseApi: databaseApi,
                option: option,
                loggerFactory: loggerFactory),
            loggerFactory: loggerFactory);
    }
}