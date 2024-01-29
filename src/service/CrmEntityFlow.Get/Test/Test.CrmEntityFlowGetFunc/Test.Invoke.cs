using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class CrmEntityFlowGetFuncTest
{
    [Fact]
    public static async Task InvokeAsync_CrmEntityFlowGetInIsNull_ExpectThrowsArgumentNullException()
    {
        var mockDataverseApi = CreateMockDataverseApi(SomeDataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseApi.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("input", ex.ParamName);

        async Task TestAsync()
            =>
            _ = await IterateOverAsyncEnumerableAsync(func, null!, cancellationToken);
    }

    [Fact]
    public static async Task InvokeAsync_CrmEntityAreEmpty_ExpectThrowsInvalidOperationException()
    {
        var dataverseEntitySetGetOut = new DataverseEntitySetGetOut<CrmEntityJson>(
            value: new(
                new CrmEntityJson
                {
                    ExtensionData = new()
                }));

        var mockDataverseApi = CreateMockDataverseApi(dataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseApi.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        _ = await Assert.ThrowsAsync<InvalidOperationException>(TestAsync);

        async Task TestAsync()
            =>
            _ = await IterateOverAsyncEnumerableAsync(func, SomeCrmEntityFlowGetIn, cancellationToken);
    }

    [Fact]
    public static async Task InvokeAsync_DataverseFailure_ExpectThrowsInvalidOperationException()
    {
        var failure = Failure.Create(DataverseFailureCode.Unknown, "Unknown error");
        var mockDataverseApi = CreateMockDataverseApi(failure);

        var func = new CrmEntityFlowGetFunc(mockDataverseApi.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        await Assert.ThrowsAsync<InvalidOperationException>(TestAsync);

        async Task TestAsync()
            =>
            _ = await IterateOverAsyncEnumerableAsync(func, SomeCrmEntityFlowGetIn, cancellationToken);
    }

    [Theory]
    [MemberData(nameof(CrmEntityFlowGetFuncSource.DataverseInputTestData), MemberType = typeof(CrmEntityFlowGetFuncSource))]
    internal static async Task InvokeAsync_CancellationTokenIsNotCanceled_ExpectDataverseEntitySetGetFunctionCall(
        CrmEntityFlowGetIn input,
        DataverseEntitySetGetOut<CrmEntityJson>[] dataverseEntitySetGetOutSet,
        DataverseEntitySetGetIn[] dataverseEntitySetGetInSet)
    {
        var mockDataverseApi = CreateMockDataverseApi(dataverseEntitySetGetOutSet);
        var func = new CrmEntityFlowGetFunc(mockDataverseApi.Object);
        var cancellationToken = new CancellationToken(canceled: false);

        _ = await IterateOverAsyncEnumerableAsync(func, input, cancellationToken);

        foreach (var dataverseEntitySetGetIn in dataverseEntitySetGetInSet)
        {
            mockDataverseApi.Verify(
                func => func.GetEntitySetAsync<CrmEntityJson>(dataverseEntitySetGetIn, cancellationToken),
                Times.Once);
        }
    }

    [Theory]
    [MemberData(nameof(CrmEntityFlowGetFuncSource.InputTestData), MemberType = typeof(CrmEntityFlowGetFuncSource))]
    internal static async Task InvokeAsync_CancellationTokenIsNotCanceled_ExpectCorrectOut(
        CrmEntityFlowGetIn input, DataverseEntitySetGetOut<CrmEntityJson> dataverseEntitySetGetOut, FlatArray<CrmEntitySet> expected)
    {
        var mockDataverseApi = CreateMockDataverseApi(dataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseApi.Object);
        var cancellationToken = new CancellationToken(canceled: false);

        var actual = await IterateOverAsyncEnumerableAsync(func, input, cancellationToken);

        Assert.True(CompareCrmEntitySets(expected, actual));
    }
}