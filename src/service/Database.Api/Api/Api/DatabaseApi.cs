using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class DatabaseApi : IDatabaseApi
{
    private const int DefaultTimeoutInSeconds = 300;

    private const string PrimaryKeyFieldName = "CrmId";

    private const string SyncTimeFieldName = "SyncTime";

    private const string DbAuditCreateTableQuery = """
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='_AuditDateTime' and xtype='U')
            BEGIN
            CREATE TABLE [_AuditDateTime]( 
                [EntityName] varchar(100) NOT NULL,
                [AuditDateTime] datetime NOT NULL,
                [CreatedAt] datetimeoffset DEFAULT SYSDATETIMEOFFSET() NOT NULL,
                PRIMARY KEY (EntityName)
            );
            END
        """;

    private readonly ISqlExecuteNonQuerySupplier sqlExecuteNonQueryApi;

    private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

    internal DatabaseApi(ISqlExecuteNonQuerySupplier sqlExecuteNonQueryApi, IDateTimeOffsetProvider dateTimeOffsetProvider)
        =>
        (this.sqlExecuteNonQueryApi, this.dateTimeOffsetProvider) = (sqlExecuteNonQueryApi, dateTimeOffsetProvider);
}