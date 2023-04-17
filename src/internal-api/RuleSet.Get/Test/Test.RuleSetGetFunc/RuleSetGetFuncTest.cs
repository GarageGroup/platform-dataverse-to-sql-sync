using System;
using System.Threading;
using Moq;

namespace GarageGroup.Platform.DataMover.Test;

public static partial class RuleSetGetTest
{
    private static readonly ConfigurationYaml SomeConfiguration
        =
        new ConfigurationYaml
        {
            Entities = new FlatArray<EntityYaml>(
                new EntityYaml
                {
                    Name = "sl_picture",
                    Plural = "sl_pictures",
                    Filter = "sl_id ne null",
                    Annotations = "OData.IncludeAnnotations",
                    Tables = new FlatArray<TableYaml>(
                        new TableYaml
                        {
                            Name = "Picture",
                            Key = "Id",
                            Fields = new FlatArray<FieldYaml>(
                                new FieldYaml
                                {
                                    Sql = "Url",
                                    Crm = "sl_url",
                                }),
                            Operation = "Update"
                        })
                })
        };

    private static readonly RuleSetGetIn SomeRuleSetGetIn
        =
        new("sl_unit", true);

    internal static Mock<IAsyncFunc<string, ConfigurationYaml>> CreateMockYamlReadFunc(ConfigurationYaml configuration)
    {
        var mock = new Mock<IAsyncFunc<string, ConfigurationYaml>>();

        _ = mock
            .Setup(static supplier => supplier.InvokeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(configuration);
        
        return mock;
    }
}