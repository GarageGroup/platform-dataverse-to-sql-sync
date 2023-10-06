using System;
using System.Collections.Generic;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class DatabaseApiTestSource
{
    public static IEnumerable<object[]> InputDeleteTestData
        =>
        new[]
        {
            new object[]
            {
                new DbDataDeleteIn("properties", new(2021, 05, 07, 12, 10, 51, TimeSpan.FromHours(-1))),
                new DateTimeOffset(2023, 2, 10, 1, 32, 1, 4, default),
                new DbDeleteQuery(
                    tableName: "properties",
                    filter: new DbParameterFilter(
                        "SyncTime", DbFilterOperator.Less, new DateTimeOffset(2021, 05, 07, 12, 10, 51, TimeSpan.FromHours(-1))))
                {
                    TimeoutInSeconds = 300
                }
            },
            new object[]
            {
                new DbDataDeleteIn(null!, new(2023, 01, 31, 05, 00, 43, TimeSpan.FromHours(5))),
                new DateTimeOffset(2022, 2, 10, 1, 32, 1, 4, TimeSpan.FromHours(3)),
                new DbDeleteQuery(
                    tableName: string.Empty,
                    filter: new DbParameterFilter(
                        "SyncTime", DbFilterOperator.Less, new DateTimeOffset(2023, 01, 31, 05, 00, 43, TimeSpan.FromHours(5))))
                {
                    TimeoutInSeconds = 300
                }
            },
            new object[]
            {
                new DbDataDeleteIn("properties", new(2017, 10, 21, 15, 45, 01, default), Guid.Parse("6f410eaa-e6b7-4bf7-9b3a-81b67276ec4b")),
                new DateTimeOffset(2021, 2, 10, 1, 32, 1, 4, default),
                new DbDeleteQuery(
                    tableName: "properties",
                    filter: new DbCombinedFilter(
                        DbLogicalOperator.And,
                        new(
                            new DbParameterFilter(
                                "CrmId", DbFilterOperator.Equal, Guid.Parse("6f410eaa-e6b7-4bf7-9b3a-81b67276ec4b")),
                            new DbParameterFilter(
                                "SyncTime", DbFilterOperator.Less, new DateTimeOffset(2017, 10, 21, 15, 45, 01, default)))))
                {
                    TimeoutInSeconds = 300
                }
            }
        };
}