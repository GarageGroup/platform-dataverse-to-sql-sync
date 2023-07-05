using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;
using Xunit;

namespace GarageGroup.Platform.DataMover.Test;

partial class DbDataApiTest
{
    [Fact]
    public static void UpdateAsync_InputIsNull_ExpectArgumentNullException()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(15);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        Assert.ThrowsAsync<ArgumentNullException>(TestAsync);

        async Task TestAsync()
            =>
            await func.UpdateAsync(null!, cancellationToken);
    }

    [Fact]
    public static void UpdateAsync_CancellationTokenIsCanceled_ExpectTaskIsCanceled()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(5);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: true);

        var result = func.UpdateAsync(SomeUpdateInput, cancellationToken);
        Assert.True(result.IsCanceled);
    }

    [Theory]
    [MemberData(nameof(DbDataApiTestSource.InputUpdateTestData), MemberType = typeof(DbDataApiTestSource))]
    public static async Task UpdateAsync_InputItemsAreNotEmpty_ExpectDbUpdateCalledOnce(
        DbDataUpdateIn input, DateTimeOffset dateTimeOffset, IDbQuery expected)
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(default);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(dateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        _ = await func.UpdateAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(expected, cancellationToken), Times.Once);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreEmpty_ExpectDbUpdateCalledNever()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(default);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);
        var input = new DbDataUpdateIn("table1", "id", new(), DbDataUpdateType.CreateOrUpdate, false);

        _ = await func.UpdateAsync(input, cancellationToken);
        mockSqlQuerySupplier.Verify(func => func.ExecuteNonQueryAsync(It.IsAny<IDbQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreNotEmpty_ExpectDbDataUpdateOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var input = new DbDataUpdateIn(
            tableName: "table1",
            keyFieldName: "id",
            items: new(
                new DbDataItem(
                    crmId: Guid.Parse("9c6c896b-506b-4bab-ba4b-546ded8d4b81"),
                    fieldValues: new(new DbDataFieldValue("field1", string.Empty)))),
            type: DbDataUpdateType.CreateOrUpdate,
            syncTime: false);

        var result = await func.UpdateAsync(input, cancellationToken);
        var expected = new DbDataUpdateOut(10);

        Assert.StrictEqual(expected, result);
    }

    [Fact]
    public static async Task UpdateAsync_InputItemsAreEmpty_ExpectDefaultDbOut()
    {
        var mockSqlQuerySupplier = CreateMockSqlQuerySupplier(10);
        var dateTimeOffsetProvider = CreateDateTimeOffsetProvider(SomeDateTimeOffset);

        var func = new DbDataApi(mockSqlQuerySupplier.Object, dateTimeOffsetProvider);
        var cancellationToken = new CancellationToken(canceled: false);

        var input = new DbDataUpdateIn(
            tableName: "table1",
            keyFieldName: "id",
            items: default,
            type: DbDataUpdateType.CreateOrUpdate,
            syncTime: false);

        var result = await func.UpdateAsync(input, cancellationToken);
        var expected = new DbDataUpdateOut(0);

        Assert.StrictEqual(expected, result);
    }
}