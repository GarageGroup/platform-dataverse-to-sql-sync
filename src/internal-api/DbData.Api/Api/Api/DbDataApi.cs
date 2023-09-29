using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class DbDataApi : IDbDataApi
{
    private const int DefaultTimeoutInSeconds = 120;

    private const string PrimaryKeyFieldName = "CrmId";

    private const string SyncTimeFieldName = "SyncTime";

    private readonly ISqlExecuteNonQuerySupplier sqlApi;

    private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

    internal DbDataApi(ISqlExecuteNonQuerySupplier sqlApi, IDateTimeOffsetProvider dateTimeOffsetProvider)
        =>
        (this.sqlApi, this.dateTimeOffsetProvider) = (sqlApi, dateTimeOffsetProvider);
}