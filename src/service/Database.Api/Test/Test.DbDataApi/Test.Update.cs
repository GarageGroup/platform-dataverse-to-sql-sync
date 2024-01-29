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
    public static async Task UpdateAsync_InputIsNull_ExpectArgumentNullException()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(15);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAsync);
        Assert.Equal("input", ex.ParamName);

        async Task TestAsync()
            =>
            await func.UpdateDataAsync(null!, cancellationToken);
    }

    [Theory]
    [MemberData(nameof(DatabaseApiTestSource.InputUpdateTestData), MemberType = typeof(DatabaseApiTestSource))]
    public static async Task UpdateAsync_InputItemsAreNotEmpty_ExpectDbUpdateCalledOnce(
        DbDataUpdateIn input, DateTimeOffset dateTimeOffset, IDbQuery expected)
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(default);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(dateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        _ = await func.UpdateDataAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(expected, cancellationToken), Times.Once);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreEmpty_ExpectDbUpdateCalledNever()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(default);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);
        var input = new DbDataUpdateIn("table1", "id", new(), DbDataUpdateType.CreateOrUpdate, false);

        _ = await func.UpdateDataAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(It.IsAny<IDbQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreNotEmpty_ExpectDbDataUpdateOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var input = new DbDataUpdateIn(
            tableName: "table1",
            keyFieldName: "id",
            items:
            [
                new(
                    crmId: Guid.Parse("9c6c896b-506b-4bab-ba4b-546ded8d4b81"),
                    fieldValues:
                    [
                        new("field1", string.Empty)
                    ])
            ],
            type: DbDataUpdateType.CreateOrUpdate,
            syncTime: false);

        var result = await func.UpdateDataAsync(input, cancellationToken);
        var expected = new DbDataUpdateOut(10);

        Assert.StrictEqual(expected, result);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreEmpty_ExpectDefaultDbOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DatabaseApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var input = new DbDataUpdateIn(
            tableName: "table1",
            keyFieldName: "id",
            items: default,
            type: DbDataUpdateType.CreateOrUpdate,
            syncTime: false);

        var result = await func.UpdateDataAsync(input, cancellationToken);
        var expected = new DbDataUpdateOut(0);

        Assert.StrictEqual(expected, result);
    }
}