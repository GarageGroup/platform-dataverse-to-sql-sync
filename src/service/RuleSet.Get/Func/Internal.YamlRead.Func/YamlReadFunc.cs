using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class YamlReadFunc : IAsyncFunc<string, ConfigurationYaml>
{
    static YamlReadFunc()
        =>
        Instance = new();

    public static YamlReadFunc Instance { get; }

    private YamlReadFunc()
    {
    }

    private static IDeserializer BuildYamlDeserializer()
        =>
        new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
}