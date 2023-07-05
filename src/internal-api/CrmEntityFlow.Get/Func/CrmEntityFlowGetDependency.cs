using System;
using System.Runtime.CompilerServices;
using GarageGroup.Infra;
using PrimeFuncPack;

[assembly: InternalsVisibleTo("GarageGroup.Platform.DataMover.InternalApi.CrmEntityFlow.Get.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace GarageGroup.Platform.DataMover;

public static class CrmEntityFlowGetDependency
{
    public static Dependency<ICrmEntityFlowGetFunc> UseCrmEntityFlowGetFunc<TDataverseApi>(this Dependency<TDataverseApi> dependency)
        where TDataverseApi : IDataverseEntitySetGetSupplier
    {
        ArgumentNullException.ThrowIfNull(dependency);

        return dependency.Map<ICrmEntityFlowGetFunc>(CreateFunc);

        static CrmEntityFlowGetFunc CreateFunc(TDataverseApi dataverseApi)
        {
            ArgumentNullException.ThrowIfNull(dataverseApi);

            return new(dataverseApi);
        }
    }
}