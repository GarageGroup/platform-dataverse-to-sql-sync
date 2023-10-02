using System;
using System.Linq;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class WriteAuditDateTimeIn
{
    public WriteAuditDateTimeIn(string entityName, DateTime auditDateTime)
        =>
        (EntityName, AuditDateTime) = (entityName, auditDateTime);

    public string EntityName { get; }

    public DateTime AuditDateTime { get; }
}