using System;
using System.Threading;
using System.Threading.Tasks;
using GGroupp.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataMover.Test;

partial class DbDataApiTest
{
    [Fact]
    public static void DeleteAsync_InputIsNull_ExpectArgumentNullException()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(21);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        Assert.ThrowsAsync<ArgumentNullException>(TestAsync);

        async Task TestAsync()
            =>
            await func.DeleteAsync(null!, cancellationToken);
    }

    [Fact]
    public static void DeleteAsync_CancellationTokenIsCanceled_ExpectTaskIsCanceled()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(15);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: true);

        var result = func.DeleteAsync(SomeDeleteInput, cancellationToken);
        Assert.True(result.IsCanceled);
    }

    [Theory]
    [MemberData(nameof(DbDataApiTestSource.InputDeleteTestData), MemberType = typeof(DbDataApiTestSource))]
    public static async Task DeleteAsync_InputIsNotNull_ExpectDbDeleteCalledOnce(
        DbDataDeleteIn input, DateTimeOffset offset, DbDeleteQuery query)
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(11);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(offset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        _ = await func.DeleteAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(query, cancellationToken), Times.Once);
    }

    [Fact]
    public static async Task DeleteAsync_CorrectData_ExpectDbDataDeleteOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var result = await func.DeleteAsync(SomeDeleteInput, cancellationToken);
        var expected = new DbDataDeleteOut(10);

        Assert.StrictEqual(expected, result);
    }
}