using System;

namespace GarageGroup.Platform.DataMover;

using IYamlReadFunc = IAsyncFunc<string, ConfigurationYaml>;

internal sealed partial class RuleSetGetFunc : IRuleSetGetFunc
{
    private readonly IYamlReadFunc yamlReadFunc;

    private readonly string configFilePath;

    private readonly string[] entityNames;

    internal RuleSetGetFunc(IYamlReadFunc yamlReadFunc, RuleSetGetOption option)
    {
        this.yamlReadFunc = yamlReadFunc;
        configFilePath = option.ConfigFilePath;
        entityNames = option.EntityNames;
    }

    private static TEnum ParseEnumOrDefault<TEnum>(string? source)
        where TEnum : struct
    {
        if (string.IsNullOrEmpty(source))
        {
            return default;
        }

        return Enum.Parse<TEnum>(source, true);
    }
}