using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal sealed partial class EntityMoveFunc : IEntityMoveFunc
{
    private const int DefaultDbBatchCount = 15;

    private const int DefaultDbBatchTimeoutMilliseconds = 7000;

    private const int MaxSqlParametersCount = 2099;

    private readonly ICrmEntityFlowGetFunc flowGetFunc;

    private readonly IDatabaseApi databaseApi;

    private readonly DataMoveOption option;

    private readonly ILogger? logger;

    internal EntityMoveFunc(
        ICrmEntityFlowGetFunc flowGetFunc,
        IDatabaseApi databaseApi,
        DataMoveOption option,
        ILoggerFactory? loggerFactory)
    {
        this.flowGetFunc = flowGetFunc;
        this.databaseApi = databaseApi;
        this.option = option;
        logger = loggerFactory?.CreateLogger<EntityMoveFunc>();
    }

    private readonly record struct MoveEntityResult(FlatArray<string> TableNames);

    private int GetCrmPageSize(int fieldCount)
        =>
        GetBatchSize(fieldCount) * GetMaxDbBatchCount();

    private static int GetBatchSize(int fieldCount)
        =>
        MaxSqlParametersCount / (fieldCount + 1);

    private int GetMaxDbBatchCount()
        =>
        option.MaxDbBatchCount > 0 ? option.MaxDbBatchCount : DefaultDbBatchCount;

    private static bool IsNotTimerExceeded(Stopwatch timer)
        =>
        timer.ElapsedMilliseconds < DefaultDbBatchTimeoutMilliseconds;

    private static async ValueTask<int> InvokeAllAsync(IReadOnlyList<Task<int>> tasks)
    {
        if (tasks.Count is 0)
        {
            return default;
        }

        if (tasks.Count is 1)
        {
            return await tasks[0].ConfigureAwait(false);
        }

        var result = await Task.WhenAll(tasks).ConfigureAwait(false);
        return result.Sum();
    }

    private static DataFieldValue GetFieldValue(CrmEntity crmEntity, RuleField ruleField)
    {
        return new(
            name: ruleField.SqlName,
            value: crmEntity.FieldValues.FirstOrAbsent(IsDataverseValueActual).Fold(InnerGetValue, GetNull));

        object? InnerGetValue(CrmEntityFieldValue fieldValue)
        {
            try
            {
                return GetValue(ruleField, fieldValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get value from {ruleField.CrmField}", ex);
            }
        }

        bool IsDataverseValueActual(CrmEntityFieldValue fieldValue)
            =>
            string.Equals(fieldValue.Name, ruleField.CrmField.Name, StringComparison.InvariantCulture);

        static object? GetNull()
            =>
            null;
    }

    private static object? GetValue(RuleField ruleField, CrmEntityFieldValue fieldValue)
    {
        var value = fieldValue.JsonValue;

        if (value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return null;
        }

        if (string.IsNullOrEmpty(ruleField.CrmField.LookupName) is false)
        {
            if (fieldValue.JsonValue.TryGetProperty(ruleField.CrmField.LookupName, out value) is false)
            {
                return null;
            }
        }

        if (value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return null;
        }

        object? dbValue = ruleField.Type switch
        {
            RuleFieldType.Int32 => GetIntOrNull(value),
            RuleFieldType.LowerString => value.GetString()?.ToLowerInvariant(),
            RuleFieldType.Decimal => value.GetDecimal(),
            RuleFieldType.Boolean => value.GetBoolean(),
            _ => value.GetString()
        };

        if (ruleField.MatcherRule.IsEmpty)
        {
            return dbValue;
        }

        return GetMatchedValue(dbValue?.ToString()).Or(GetDefaultOrAbsent).Or(SkipOrAbsent).OrThrow(UnableToFindException);

        InvalidOperationException UnableToFindException()
            =>
            new($"Unable to find value '{dbValue}' with passed key for property {ruleField.CrmField.Name}");

        Optional<string?> GetDefaultOrAbsent()
            =>
            GetMatchedValue("_");

        Optional<string?> SkipOrAbsent()
            =>
            ruleField.SkipNullable ? Optional.Present<string?>(null) : default;

        Optional<string?> GetMatchedValue(string? key)
        {
            return ruleField.MatcherRule.FirstOrAbsent(IsMatched).Map(GetValue);

            bool IsMatched(KeyValuePair<string, string?> pair)
                =>
                string.Equals(pair.Key, key, StringComparison.InvariantCulture);

            static string? GetValue(KeyValuePair<string, string?> pair)
                =>
                pair.Value;
        }

        static object? GetIntOrNull(JsonElement value)
        {
            if (value.ValueKind is JsonValueKind.String)
            {
                return int.TryParse(value.GetString(), out var stringValue) ? stringValue : null;
            }

            if (value.ValueKind is not JsonValueKind.Number)
            {
                return null;
            }

            if (value.TryGetInt32(out var intValue))
            {
                return intValue;
            }

            return value.TryGetDecimal(out var decimalValue) ? (int)decimalValue : null;
        }
    }
}