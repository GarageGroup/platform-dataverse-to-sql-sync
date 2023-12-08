using System;
using System.Threading;
using Moq;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

public static partial class RuleSetGetTest
{
    private static readonly ConfigurationYaml SomeConfiguration
        =
        new()
        {
            Entities =
            [
                new()
                {
                    Name = "sl_picture",
                    Plural = "sl_pictures",
                    Filter = "sl_id ne null",
                    Annotations = "OData.IncludeAnnotations",
                    Tables =
                    [
                        new()
                        {
                            Name = "Picture",
                            Key = "Id",
                            Fields =
                            [
                                new()
                                {
                                    Sql = "Url",
                                    Crm = "sl_url",
                                }
                            ],
                            Operation = "Update"
                        }
                    ]
                }
            ]
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