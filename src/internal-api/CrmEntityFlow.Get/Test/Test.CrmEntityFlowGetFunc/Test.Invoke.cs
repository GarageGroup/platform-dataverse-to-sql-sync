using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataMover.Test;

partial class CrmEntityFlowGetFuncTest
{
    [Fact]
    public static async Task InvokeAsync_CrmEntityFlowGetInIsNull_ExpectThrowsArgumentNullException()
    {
        var mockDataverseEntitySetGetSupplier = CreateMockDataverseEntitySetGetSupplier(SomeDataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseEntitySetGetSupplier.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await IterateOverAsyncEnumerableAsync(func, null!, cancellationToken));
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
        var mockDataverseEntitySetGetSupplier = CreateMockDataverseEntitySetGetSupplier(dataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseEntitySetGetSupplier.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await IterateOverAsyncEnumerableAsync(func, SomeCrmEntityFlowGetIn, cancellationToken));
    }

    [Fact]
    public static async Task InvokeAsync_DataverseFailure_ExpectThrowsInvalidOperationException()
    {
        var mockDataverseEntitySetGetSupplier = CreateMockDataverseEntitySetGetSupplier(Failure.Create(DataverseFailureCode.Unknown, "Unknown error"));

        var func = new CrmEntityFlowGetFunc(mockDataverseEntitySetGetSupplier.Object);
        var cancellationToken = new CancellationToken(canceled: true);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await IterateOverAsyncEnumerableAsync(func, SomeCrmEntityFlowGetIn, cancellationToken));
    }

    [Theory]
    [MemberData(nameof(CrmEntityFlowGetFuncSource.InputTestData), MemberType = typeof(CrmEntityFlowGetFuncSource))]
    internal static async Task InvokeAsync_CancellationTokenIsNotCanceled_ExpectCorrectOut(
        CrmEntityFlowGetIn input, DataverseEntitySetGetOut<CrmEntityJson> dataverseEntitySetGetOut, FlatArray<CrmEntitySet> expected)
    {
        var mockDataverseEntitySetGetSupplier = CreateMockDataverseEntitySetGetSupplier(dataverseEntitySetGetOut);

        var func = new CrmEntityFlowGetFunc(mockDataverseEntitySetGetSupplier.Object);
        var cancellationToken = new CancellationToken(canceled: false);

        var actual = await IterateOverAsyncEnumerableAsync(func, input, cancellationToken);

        Assert.True(CompareCrmEntitySets(expected, actual));
    }

    [Theory]
    [MemberData(nameof(CrmEntityFlowGetFuncSource.DataverseInputTestData), MemberType = typeof(CrmEntityFlowGetFuncSource))]
    internal static async Task InvokeAsync_CancellationTokenIsNotCanceled_ExpectDataverseEntitySetGetFunctionCall(
        CrmEntityFlowGetIn input, FlatArray<DataverseEntitySetGetOut<CrmEntityJson>> dataverseEntitySetGetOutSet, FlatArray<DataverseEntitySetGetIn> dataverseEntitySetGetInSet)
    {
        var mockDataverseEntitySetGetSupplier = CreateMockDataverseEntitySetGetSupplier(dataverseEntitySetGetOutSet);
        var func = new CrmEntityFlowGetFunc(mockDataverseEntitySetGetSupplier.Object);
        var cancellationToken = new CancellationToken(canceled: false);

        var _ = await IterateOverAsyncEnumerableAsync(func, input, cancellationToken);

        foreach (var dataverseEntitySetGetIn in dataverseEntitySetGetInSet)
        {
            mockDataverseEntitySetGetSupplier.Verify(func => func.GetEntitySetAsync<CrmEntityJson>(dataverseEntitySetGetIn, cancellationToken), Times.Once);
        }
    }
}