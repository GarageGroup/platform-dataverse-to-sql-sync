using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class DatabaseApiTest
{
    [Fact]
    public static async Task DeleteAsync_InputIsNull_ExpectArgumentNullException()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(21);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("input", ex.ParamName);

        async Task TestAsync()
            =>
            await func.DeleteDataAsync(null!, cancellationToken);
    }

    [Theory]
    [MemberData(nameof(DatabaseApiTestSource.InputDeleteTestData), MemberType = typeof(DatabaseApiTestSource))]
    public static async Task DeleteAsync_InputIsNotNull_ExpectDbDeleteCalledOnce(
        DbDataDeleteIn input, DateTimeOffset offset, DbDeleteQuery query)
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(11);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(offset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        _ = await func.DeleteDataAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(query, cancellationToken), Times.Once);
    }

    [Fact]
    public static async Task DeleteAsync_CorrectData_ExpectDbDataDeleteOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var result = await func.DeleteDataAsync(SomeDeleteInput, cancellationToken);
        var expected = new DbDataDeleteOut(10);

        Assert.StrictEqual(expected, result);
    }
}