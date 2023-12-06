using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class DatabaseApi(ISqlExecuteNonQuerySupplier sqlApi, IDateTimeOffsetProvider dateTimeOffsetProvider) : IDatabaseApi
{
    private const int DefaultTimeoutInSeconds = 300;

    private const string PrimaryKeyFieldName = "CrmId";

    private const string SyncTimeFieldName = "SyncTime";

    private const string AuditDateTimeTableName = "_AuditDateTime";

    private static readonly DbQuery DbAuditCreateTableQuery;

    static DatabaseApi()
        =>
        DbAuditCreateTableQuery = new(
            $"""
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{AuditDateTimeTableName}' and xtype='U')
                    BEGIN
                    CREATE TABLE [{AuditDateTimeTableName}]( 
                        [EntityName] varchar(100) NOT NULL,
                        [AuditDateTime] datetime NOT NULL,
                        [CreatedAt] datetimeoffset DEFAULT SYSDATETIMEOFFSET() NOT NULL,
                        PRIMARY KEY (EntityName)
                    );
                    END
            """);
}