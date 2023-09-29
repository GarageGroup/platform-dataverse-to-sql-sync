using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

public sealed record class RuleEntity
{
    public RuleEntity(RuleCrmData crmData, FlatArray<RuleTable> tables)
    {
        CrmData = crmData;
        Tables = tables;
    }

    public RuleCrmData CrmData { get; }

    public FlatArray<RuleTable> Tables { get; }
}