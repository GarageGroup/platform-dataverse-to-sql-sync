using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataMover;

partial class RuleSetGetFunc
{
    public ValueTask<RuleSetGetOut> InvokeAsync(RuleSetGetIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            configFilePath, cancellationToken)
        .HandleCancellation()
        .Pipe(
            yamlReadFunc.InvokeAsync)
        .Pipe(
            configuration => GetEntities(configuration, input))
        .Pipe(
            static entities => new RuleSetGetOut(entities));

    private FlatArray<RuleEntity> GetEntities(ConfigurationYaml configuration, RuleSetGetIn input)
    {
        var configurationEntities = configuration.Entities;
        if (configurationEntities?.Length is not > 0 || entityNames.Length is not > 0)
        {
            return default;
        }

        var entityCollection = configurationEntities.Where(NotWhiteSpaceEntityName);
        if (entityNames.Length is not 1 || entityNames[0] is not "*")
        {
            entityCollection = entityCollection.Where(IsEntityNameAllowed);
        }

        if (string.IsNullOrWhiteSpace(input.CrmEntityName) is false)
        {
            entityCollection = entityCollection.Where(IsEntityNameExpected);
        }

        return entityCollection.ToFlatArray().FlatMap(InnerMapEntity);

        static bool NotWhiteSpaceEntityName(EntityYaml entity)
            =>
            string.IsNullOrWhiteSpace(entity.Name) is false;

        bool IsEntityNameAllowed(EntityYaml entity)
            =>
            entityNames.Contains(entity.Name, StringComparer.InvariantCulture);

        bool IsEntityNameExpected(EntityYaml entity)
            =>
            string.Equals(entity.Name, input.CrmEntityName, StringComparison.InvariantCulture);

        Optional<RuleEntity> InnerMapEntity(EntityYaml entity)
            =>
            MapEntity(entity, input.SyncOnly);
    }

    private static Optional<RuleEntity> MapEntity(EntityYaml entity, bool syncOnly)
    {
        if (entity.Tables?.Length is not > 0)
        {
            return default;
        }

        var crmEntityName = entity.Name?.Trim() ?? string.Empty;
        var entityKeyFieldName = crmEntityName + "id";

        var tablesCollection = entity.Tables.Select(InnerMapTable);
        if (syncOnly)
        {
            tablesCollection = tablesCollection.Where(IsSyncOperation);
        }

        var tables = tablesCollection.ToArray();

        return new RuleEntity(
            crmData: new(
                entityName: crmEntityName,
                entityPluralName: entity.Plural?.Trim().OrNullIfEmpty() ?? crmEntityName + "s",
                entityKeyFieldName: entityKeyFieldName,
                fields: tables.SelectMany(GetCrmFields).Distinct().ToFlatArray(),
                filter: entity.Filter?.Trim(),
                includeAnnotations: CalculateAnnotations(entity)),
            tables: tables);

        RuleTable InnerMapTable(TableYaml table)
            =>
            MapTable(table, entityKeyFieldName);

        static bool IsSyncOperation(RuleTable rule)
            =>
            rule.Operation is RuleItemOperation.Sync;

        static IEnumerable<RuleCrmField> GetCrmFields(RuleTable table)
            =>
            table.Fields.AsEnumerable().Select(GetCrmField);

        static RuleCrmField GetCrmField(RuleField field)
            =>
            field.CrmField;
    }

    private static string? CalculateAnnotations(EntityYaml entity)
    {
        var annotations = entity.Annotations?.Trim();
        if(string.IsNullOrEmpty(annotations) is false)
        {
            return annotations;
        }

        var annotationsArray = entity.Tables?
            .SelectMany(
                static table => table?.Fields?.SelectMany(ExtractAnnotation) ?? Enumerable.Empty<string>())
            .Distinct()
            .ToArray();
        
        return annotationsArray is { Length: > 0 } ? string.Join(",", annotationsArray) : null;

        static IEnumerable<string> ExtractAnnotation(FieldYaml field)
        {
            const char annotationDelimiter = '@';
            const int splitLength = 2;
            const int annotationIndex = 1;

            if (field.Crm?.Trim().Split(annotationDelimiter) is { Length: splitLength } splittedCrm
                && string.IsNullOrEmpty(splittedCrm[annotationIndex]) is false)
            {
                yield return splittedCrm[annotationIndex];
            }

            if (field.LookupName?.Trim().Split(annotationDelimiter) is { Length: splitLength } splittedLookupName
                && string.IsNullOrEmpty(splittedLookupName[annotationIndex]) is false)
            {
                yield return splittedLookupName[annotationIndex];
            }
        }
    }

    private static RuleTable MapTable(TableYaml table, string entityKeyFieldName)
    {
        var operation = ParseEnumOrDefault<RuleItemOperation>(table.Operation);

        var sqlKeyFieldName = table.Key?.Trim().OrNullIfEmpty() ?? "CrmId";
        var fields = table.Fields?.Select(MapField).ToList() ?? new();

        if (operation is RuleItemOperation.Sync)
        {
            var sqlFieldNames = fields.Select(GetSqlName).ToArray();
            fields.AddRange(CreateAdditionFields(entityKeyFieldName, sqlKeyFieldName).Where(IsNotConfigured));

            bool IsNotConfigured(RuleField field)
                =>
                sqlFieldNames.Contains(field.SqlName, StringComparer.InvariantCulture) is false;
        }

        return new(
            operation: operation,
            tableName: table.Name.OrEmpty().Trim(),
            keyFieldName: sqlKeyFieldName,
            fields: fields);

        static string GetSqlName(RuleField field)
            =>
            field.SqlName;
    }

    private static RuleField MapField(FieldYaml field)
        =>
        new(
            crmField: new(field.Crm.OrEmpty().Trim(), field.LookupName?.Trim()),
            sqlName: field.Sql.OrEmpty().Trim(),
            type: ParseEnumOrDefault<RuleFieldType>(field.Type),
            matcherRule: field.Map?.ToArray(),
            skipNullable: field.SkipNullable);

    private static RuleField[] CreateAdditionFields(string crmEntityKeyFieldName, string sqlKeyFieldName)
        =>
        new RuleField[]
        {
            new(
                crmField: new(crmEntityKeyFieldName),
                sqlName: sqlKeyFieldName,
                type: default,
                matcherRule: default,
                skipNullable: true),
            new(
                crmField: new("createdon"),
                sqlName: "CrmCreationTime",
                type: default,
                matcherRule: default,
                skipNullable: false),
            new(
                crmField: new("modifiedon"),
                sqlName: "CrmModifiedTime",
                type: default,
                matcherRule: default,
                skipNullable: false)
        };
}