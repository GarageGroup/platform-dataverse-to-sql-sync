using System;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed record class EntityMoveIn
{
    public EntityMoveIn(RuleEntity rule, Guid? crmEntityId, bool deleteOnly, bool moveOnly)
    {
        Rule = rule;
        CrmEntityId = crmEntityId;
        DeleteOnly = deleteOnly;
        MoveOnly = moveOnly;
    }

    public RuleEntity Rule { get; }

    public Guid? CrmEntityId { get; }

    public bool DeleteOnly { get; }

    public bool MoveOnly { get; }
}