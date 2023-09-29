using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class RuleSetGetTest
{
    [Fact]
    public static void InvokeAsync_CancellationTokenIsCanceled_ExpectTaskIsCanceled()
    {
        var mockYamlReadFunc = CreateMockYamlReadFunc(SomeConfiguration);

        var func = new RuleSetGetFunc(mockYamlReadFunc.Object, new(string.Empty, default));
        var cancellationToken = new CancellationToken(canceled: true);

        var result = func.InvokeAsync(SomeRuleSetGetIn, cancellationToken);
        Assert.True(result.IsCanceled);
    }

    [Fact]
    public static void InvokeAsync_CancellationTokenIsNotCanceled_ExpectYamlReaderWithPassedArguments()
    {
        const string filePath = "./config.yaml";
        var mockYamlReadFunc = CreateMockYamlReadFunc(SomeConfiguration);

        var func = new RuleSetGetFunc(mockYamlReadFunc.Object, new(filePath, default));
        var cancellationToken = new CancellationToken(canceled: false);

        var _ = func.InvokeAsync(SomeRuleSetGetIn, cancellationToken);
        mockYamlReadFunc.Verify(func => func.InvokeAsync(filePath, cancellationToken), Times.Once);
    }

    [Theory]
    [MemberData(nameof(RuleSetGetTestSource.InputTestData), MemberType = typeof(RuleSetGetTestSource))]
    internal static async Task InvokeAsync_CancellationTokenIsNotCanceled_ExpectCorrectOut(
        RuleSetGetOption option, ConfigurationYaml configuration, RuleSetGetIn input, RuleSetGetOut expected)
    {
        var mockYamlReadFunc = CreateMockYamlReadFunc(configuration);

        var func = new RuleSetGetFunc(mockYamlReadFunc.Object, option);
        var cancellationToken = new CancellationToken(canceled: false);

        var actual = await func.InvokeAsync(input, cancellationToken);
        Assert.Equal(expected, actual);
    }
}