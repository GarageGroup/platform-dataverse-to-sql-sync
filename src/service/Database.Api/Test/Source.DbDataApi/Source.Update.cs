using System;
using System.Collections.Generic;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class DatabaseApiTestSource
{
    public static IEnumerable<object[]> InputUpdateTestData
        =>
        new object[][]
        {
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "golden plaza"),
                                new("id", "856"),
                                new("listingId", "1")
                            })
                    },
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbIfQuery[]
                    {
                        new(
                            condition: new DbExistsFilter(
                                selectQuery: new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        fieldName: "id",
                                        @operator: DbFilterOperator.Equal,
                                        fieldValue: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                                        parameterName: "id0")
                                }),
                            thenQuery: new DbUpdateQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0"),
                                    new("listingId", "1", "listingId0")
                                },
                                filter: new DbParameterFilter(
                                    fieldName: "id",
                                    @operator: DbFilterOperator.Equal,
                                    fieldValue: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                                    parameterName: "id0")),
                            elseQuery: new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0"),
                                    new("id", "856", "id0"),
                                    new("listingId", "1", "listingId0")
                                }))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "golden plaza"),
                                new("id", "856")
                            })
                    },
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2023, 2, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbIfQuery[]
                    {
                        new(
                            condition: new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        fieldName: "id",
                                        @operator: DbFilterOperator.Equal,
                                        fieldValue: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                                        parameterName: "id0")
                                }),
                            thenQuery: new DbUpdateQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0")
                                },
                                filter: new DbParameterFilter(
                                    fieldName: "id",
                                    @operator: DbFilterOperator.Equal,
                                    fieldValue: Guid.Parse("d257fb12-d9ab-4d46-ae22-db5204c52cfa"),
                                    parameterName: "id0")),
                            elseQuery: new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "golden plaza", "name0"),
                                    new("id", "856", "id0")
                                }))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "silver light"),
                                new("id", "21")
                            })
                    },
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2021, 2, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbUpdateQuery[]
                    {
                        new(
                            tableName: "properties",
                            fieldValues: new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            filter: new DbParameterFilter(
                                fieldName: "id",
                                @operator: DbFilterOperator.Equal,
                                fieldValue: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                                parameterName: "id0"))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "silver light"),
                                new("id", "21")
                            })
                    },
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 15, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbUpdateQuery[]
                    {
                        new(
                            tableName: "properties",
                            fieldValues: new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            filter: new DbParameterFilter(
                                fieldName: "id",
                                @operator: DbFilterOperator.Equal,
                                fieldValue: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                                parameterName: "id0"))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "silver light"),
                                new("id", "21")
                            }),
                        new(
                            crmId: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "Big ben"),
                                new("id", "12")
                            })
                    },
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: false),
                new DateTimeOffset(2022, 2, 11, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbUpdateQuery[]
                    {
                        new(
                            tableName: "properties",
                            fieldValues: new DbFieldValue[]
                            {
                                new("name", "silver light", "name0")
                            },
                            filter: new DbParameterFilter(
                                fieldName: "id",
                                @operator: DbFilterOperator.Equal,
                                fieldValue: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                                parameterName: "id0")),
                        new(
                            tableName: "properties",
                            fieldValues: new DbFieldValue[]
                            {
                                new("name", "Big ben", "name1")
                            },
                            filter: new DbParameterFilter(
                                fieldName: "id",
                                @operator: DbFilterOperator.Equal,
                                fieldValue: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                                parameterName: "id1"))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "silver light"),
                                new("id", "21")
                            }),
                        new(
                            crmId: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "Big ben"),
                                new("id", "12")
                            })
                    },
                    type: DbDataUpdateType.CreateOrUpdate,
                    syncTime: false),
                new DateTimeOffset(2022, 6, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbIfQuery[]
                    {
                        new(
                            condition: new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        fieldName: "id",
                                        @operator: DbFilterOperator.Equal,
                                        fieldValue: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                                        parameterName: "id0")
                                }),
                            thenQuery: new DbUpdateQuery(
                                "properties",
                                new DbFieldValue[]
                                {
                                    new("name", "silver light", "name0")
                                },
                                new DbParameterFilter(
                                    fieldName: "id",
                                    @operator: DbFilterOperator.Equal,
                                    fieldValue: Guid.Parse("00f1096e-a3f3-44dd-89e0-e3acae0e9f3e"),
                                    parameterName: "id0")),
                            elseQuery: new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "silver light", "name0"),
                                    new("id", "21", "id0")
                                })),
                        new(
                            condition: new DbExistsFilter(
                                new("properties")
                                {
                                    SelectedFields = new("id"),
                                    Filter = new DbParameterFilter(
                                        fieldName: "id",
                                        @operator: DbFilterOperator.Equal,
                                        fieldValue: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                                        parameterName: "id1")
                                }),
                            thenQuery: new DbUpdateQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "Big ben", "name1")
                                },
                                filter: new DbParameterFilter(
                                    fieldName: "id",
                                    @operator: DbFilterOperator.Equal,
                                    fieldValue: Guid.Parse("c4782fb7-496c-44b1-82fa-78aaefb21dc8"),
                                    parameterName: "id1")),
                            elseQuery: new DbInsertQuery(
                                tableName: "properties",
                                fieldValues: new DbFieldValue[]
                                {
                                    new("name", "Big ben", "name1"),
                                    new("id", "12", "id1")
                                }))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
            [
                new DbDataUpdateIn(
                    tableName: "properties",
                    keyFieldName: "id",
                    items: new DbDataItem[]
                    {
                        new(
                            crmId: Guid.Parse("c70fba50-d08b-4ccf-9fa8-129dcbfe4037"),
                            fieldValues: new DataFieldValue[]
                            {
                                new("name", "silver light"),
                                new("id", "21")
                            })
                    },
                    type: DbDataUpdateType.UpdateOnly,
                    syncTime: true),
                new DateTimeOffset(2022, 8, 10, 1, 32, 1, 4, default),
                new DbCombinedQuery(
                    queries: new DbUpdateQuery[]
                    {
                        new(
                            "properties",
                            new DbFieldValue[]
                            {
                                new("name", "silver light", "name0"),
                                new("SyncTime", new DateTimeOffset(2022, 8, 10, 1, 32, 1, 4, default), "SyncTime0")
                            },
                            new DbParameterFilter(
                                fieldName: "id",
                                @operator: DbFilterOperator.Equal,
                                fieldValue: Guid.Parse("c70fba50-d08b-4ccf-9fa8-129dcbfe4037"),
                                parameterName: "id0"))
                    })
                {
                    TimeoutInSeconds = 300
                }
            ],
        };
}