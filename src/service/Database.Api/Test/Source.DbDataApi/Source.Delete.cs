using System;
using GarageGroup.Infra;
using Xunit;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class DatabaseApiTestSource
{
    public static TheoryData<DbDataDeleteIn, DateTimeOffset, DbDeleteQuery> InputDeleteTestData
        =>
        new()
        {
            {
                new("properties", new(2021, 05, 07, 12, 10, 51, TimeSpan.FromHours(-1))),
                new(2023, 2, 10, 1, 32, 1, 4, default),
                new(
                    tableName: "properties",
                    filter: new DbParameterFilter(
                        fieldName: "SyncTime",
                        @operator: DbFilterOperator.Less,
                        fieldValue: new DateTimeOffset(2021, 05, 07, 12, 10, 51, TimeSpan.FromHours(-1))))
            },
            {
                new(null!, new(2023, 01, 31, 05, 00, 43, TimeSpan.FromHours(5))),
                new(2022, 2, 10, 1, 32, 1, 4, TimeSpan.FromHours(3)),
                new(
                    tableName: string.Empty,
                    filter: new DbParameterFilter(
                        fieldName: "SyncTime",
                        @operator: DbFilterOperator.Less,
                        fieldValue: new DateTimeOffset(2023, 01, 31, 05, 00, 43, TimeSpan.FromHours(5))))
            },
            {
                new("properties", new(2017, 10, 21, 15, 45, 01, default), Guid.Parse("6f410eaa-e6b7-4bf7-9b3a-81b67276ec4b")),
                new(2021, 2, 10, 1, 32, 1, 4, default),
                new(
                    tableName: "properties",
                    filter: new DbCombinedFilter(DbLogicalOperator.And)
                    {
                        Filters =
                        [
                            new DbParameterFilter("CrmId", DbFilterOperator.Equal, Guid.Parse("6f410eaa-e6b7-4bf7-9b3a-81b67276ec4b")),
                            new DbParameterFilter("SyncTime", DbFilterOperator.Less, new DateTimeOffset(2017, 10, 21, 15, 45, 01, default))
                        ]
                    })
            }
        };
}