using System;
using System.Threading;
using GarageGroup.Infra;
using Moq;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

public static partial class DatabaseApiTest
{
    private static readonly DbDataDeleteIn SomeDeleteInput
        =
        new(
            tableName: "properties",
            syncTime: new(new DateTime(2023, 2, 26)),
            crmId: Guid.Parse("6f277ed9-8b60-497b-88d8-3a83e1b07e21"));

    private static readonly DbDataUpdateIn SomeUpdateInput
        =
        new(
            tableName: "properties",
            keyFieldName: "CrmId",
            items: new(
                new DbDataItem(
                    Guid.Parse("a90d93fe-ba9e-4deb-93e0-d2b8e52ccc32"),
                    new(
                        new DataFieldValue("someName", "someValue")))),
            type: DbDataUpdateType.CreateOrUpdate,
            syncTime: true);

    private static readonly DateTimeOffset SomeDateTimeOffset
        =
        new(2022, 2, 10, 1, 32, 1, 4, default);

    private static Mock<ISqlExecuteNonQuerySupplier> CreateMockSqlQuerySupplier(int affectedRows)
    {
        var mock = new Mock<ISqlExecuteNonQuerySupplier>();

        _ = mock
            .Setup(static supplier => supplier.ExecuteNonQueryAsync(It.IsAny<IDbQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(affectedRows);

        return mock;
    }

    private static IDateTimeOffsetProvider CreateDateTimeOffsetProvider(DateTimeOffset offset)
        =>
        Mock.Of<IDateTimeOffsetProvider>(p => p.Now == offset);
}