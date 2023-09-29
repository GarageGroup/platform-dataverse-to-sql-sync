using System;
using System.Collections.Generic;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class DatabaseApiTestSource
{
    public static IEnumerable<object[]> InputUpdateTestData
        =>
        new[]
        {
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new(
                        new DbDataItem(
                            crmId: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                            fieldValues: new(
                                new("name", "golden plaza"),
                                new("id", "856"),
                                new("listingId", "1")))),
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    new(
                        new DbIfQuery(
                            new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        "id", DbFilterOperator.Equal, Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"), "id0")
                                }),
                            new DbUpdateQuery(
                                "properties",
                                new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0"),
                                    new("listingId", "1", "listingId0")
                                },
                                new DbParameterFilter(
                                    "id", DbFilterOperator.Equal, Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"), "id0")),
                            new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new(
                                    new("name", "golden plaza", "name0"),
                                    new("id", "856", "id0"),
                                    new("listingId", "1", "listingId0"))))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new FlatArray<DbDataItem>(
                        new DbDataItem(
                            crmId: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                            fieldValues: new(
                                new("name", "golden plaza"),
                                new("id", "856")))),
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2023, 2, 10, 1, 32, 1, 4, default(TimeSpan)),
                new DbCombinedQuery(
                    new(
                        new DbIfQuery(
                            new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        "id", DbFilterOperator.Equal, Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"), "id0")
                                }),
                            new DbUpdateQuery(
                                "properties",
                                new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0")
                                },
                                new DbParameterFilter(
                                    "id", DbFilterOperator.Equal, Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"), "id0")),
                            new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new(
                                    new("name", "golden plaza", "name0"),
                                    new("id", "856", "id0"))))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new FlatArray<DbDataItem>(
                        new DbDataItem(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new(
                                new("name", "silver light"),
                                new("id", "21")))),
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2021, 2, 10, 1, 32, 1, 4, default(TimeSpan)),
                new DbCombinedQuery(
                    new(
                        new DbUpdateQuery(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            new DbParameterFilter(
                                "id", DbFilterOperator.Equal, Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"), "id0"))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new FlatArray<DbDataItem>(
                        new DbDataItem(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new(
                                new("name", "silver light"),
                                new("id", "21")))),
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 15, 1, 32, 1, 4, default(TimeSpan)),
                new DbCombinedQuery(
                    new(
                        new DbUpdateQuery(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            new DbParameterFilter(
                                "id", DbFilterOperator.Equal, Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"), "id0"))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new FlatArray<DbDataItem>(
                        new DbDataItem(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new(
                                new("name", "silver light"),
                                new("id", "21"))),
                        new DbDataItem(
                            crmId: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                            fieldValues: new(
                                new("name", "Big ben"),
                                new("id", "12")))),
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 11, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    new(
                        new DbUpdateQuery(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            new DbParameterFilter(
                                "id", DbFilterOperator.Equal, Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"), "id0")),
                        new DbUpdateQuery(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "Big ben", "name1")
                            },
                            new DbParameterFilter(
                                "id", DbFilterOperator.Equal, Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"), "id1"))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new(
                        new DbDataItem(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new(
                                new("name", "silver light"),
                                new("id", "21"))),
                        new DbDataItem(
                            crmId: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                            fieldValues: new(
                                new("name", "Big ben"),
                                new("id", "12")))),
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2022, 6, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    new(
                        new DbIfQuery(
                            new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        "id", DbFilterOperator.Equal, Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"), "id0")
                                }),
                            new DbUpdateQuery(
                                "properties",
                                new DbFieldValue[]
                                {
                                    new("name", "silver light", "name0")
                                },
                                new DbParameterFilter(
                                    "id", DbFilterOperator.Equal, Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"), "id0")),
                            new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new(
                                    new("name", "silver light", "name0"),
                                    new("id", "21", "id0")))),
                        new DbIfQuery(
                            new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        "id", DbFilterOperator.Equal, Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"), "id1")
                                }),
                            new DbUpdateQuery(
                                "properties",
                                new DbFieldValue[]
                                {
                                    new("name", "Big ben", "name1")
                                },
                                new DbParameterFilter(
                                    "id", DbFilterOperator.Equal, Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"), "id1")),
                            new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new(
                                    new("name", "Big ben", "name1"),
                                    new("id", "12", "id1"))))))
                {
                    TimeoutInSeconds = 120
                }
            },
            new object[]
            {
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new(
                        new DbDataItem(
                            crmId: Guid.Parse("c70fba50-d08b-4ccf-9fa8-129dcbfe4037"),
                            fieldValues: new(
                                new DataFieldValue("name", "silver light"),
                                new DataFieldValue("id", "21")))),
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: true),
                new DateTimeOffset(2022, 8, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    new(
                        new DbUpdateQuery(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "silver light", "name0"),
                                new("SyncTime", new DateTimeOffset(2022, 8, 10, 1, 32, 1, 4, default), "SyncTime0")
                            },
                            new DbParameterFilter(
                                "id", DbFilterOperator.Equal, Guid.Parse("c70fba50-d08b-4ccf-9fa8-129dcbfe4037"), "id0"))))
                {
                    TimeoutInSeconds = 120
                }
            },
        };
}