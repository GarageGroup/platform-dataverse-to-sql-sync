using System;
using System.Collections.Generic;

namespace GarageGroup.Platform.DataMover;

public sealed record class RuleField
{
    public RuleField(
        RuleCrmField crmField,
        string sqlName,
        RuleFieldType type,
        FlatArray<KeyValuePair<string, string?>> matcherRule,
        bool skipNullable)
    {
        CrmField = crmField;
        SqlName = sqlName ?? string.Empty;
        Type = type;
        MatcherRule = matcherRule;
        SkipNullable = skipNullable;
    }

    public RuleCrmField CrmField { get; }

    public string SqlName { get; }

    public RuleFieldType Type { get; }

    public FlatArray<KeyValuePair<string, string?>> MatcherRule { get; }

    public bool SkipNullable { get; }
}