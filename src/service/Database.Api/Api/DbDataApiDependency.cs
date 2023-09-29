using System;
using System.Runtime.CompilerServices;
using GarageGroup.Infra;
using PrimeFuncPack;

[assembly: InternalsVisibleTo("GarageGroup.Platform.DataverseToSqlSync.InternalApi.DbData.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace GarageGroup.Platform.DataverseToSqlSync;

public static class DatabaseApiDependency
{
    public static Dependency<IDatabaseApi> UseDatabaseApi<TSqlApi>(this Dependency<TSqlApi> dependency)
        where TSqlApi : ISqlExecuteNonQuerySupplier
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IDatabaseApi>(Create);

        static DatabaseApi Create(TSqlApi sqlApi)
        {
            ArgumentNullException.ThrowIfNull(sqlApi);
            return new(sqlApi, DateTimeOffsetProvider.Instance);
        }
    }
}