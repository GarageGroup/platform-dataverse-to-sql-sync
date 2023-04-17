using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GarageGroup.Platform.DataMover;

internal sealed partial class YamlReadFunc : IAsyncFunc<string, ConfigurationYaml>
{
    private static readonly IDeserializer YamlDeserializer;

    static YamlReadFunc()
    {
        YamlDeserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        Instance = new();
    }

    public static YamlReadFunc Instance { get; }

    private YamlReadFunc()
    {
    }
}