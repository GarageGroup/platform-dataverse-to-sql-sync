using System;
using System.Runtime.CompilerServices;
using PrimeFuncPack;

[assembly: InternalsVisibleTo("GarageGroup.Platform.DataMover.InternalApi.RuleSet.Get.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace GarageGroup.Platform.DataMover;

public static class RuleSetGetDependency
{
    public static Dependency<IRuleSetGetFunc> UseRuleSetGetFunc(this Dependency<RuleSetGetOption> dependency)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IRuleSetGetFunc>(CreateRuleSetGetFunc);
    }

    private static RuleSetGetFunc CreateRuleSetGetFunc(RuleSetGetOption option)
    {
        ArgumentNullException.ThrowIfNull(option);
        return new(YamlReadFunc.Instance, option);
    }
}