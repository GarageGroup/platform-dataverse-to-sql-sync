using System;
using System.Collections.Generic;

namespace GarageGroup.Platform.DataMover.Test;

partial class RuleSetGetTestSource
{
    public static IEnumerable<object[]> InputTestData
        =>
        new[]
        {
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", default),
                new ConfigurationYaml(),
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(default),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", default),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Key = "Id",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Update"
                                })
                        })
                },
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(default),
            },
            new object[]
            {
                new RuleSetGetOption(
                    "./configuration.yaml",
                    new("sl_pictures", "sl_unit")
                ),
                new ConfigurationYaml(),
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Key = "CrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Sync"
                                })
                        },
                        new EntityYaml
                        {
                            Name = "sl_unit",
                            Plural = "sl_units",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Property",
                                    Key = "CrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields: new(
                                    new RuleCrmField("sl_url"),
                                    new RuleCrmField("sl_pictureid"),
                                    new RuleCrmField("createdon"),
                                    new RuleCrmField("modifiedon")),
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Sync,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("sl_pictureid"),
                                            sqlName: "CrmId",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: true),
                                        new RuleField(
                                            crmField: new("createdon"),
                                            sqlName: "CrmCreationTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("modifiedon"),
                                            sqlName: "CrmModifiedTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))),
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_unit",
                                entityPluralName: "sl_units",
                                entityKeyFieldName: "sl_unitid",
                                fields: new(
                                    new RuleCrmField("sl_name")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Property",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_unit  \n\n",
                            Plural = "sl_units",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Property",
                                    Key = "CrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_unit",
                                entityPluralName: "sl_units",
                                entityKeyFieldName: "sl_unitid",
                                fields: new(
                                    new RuleCrmField("sl_name")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Property",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null \n\n   \n",
                            Annotations = "OData.IncludeAnnotations    \n\n",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Key = "CrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Sync"
                                },
                                new TableYaml
                                {
                                    Name = "Listing",
                                    Key = "PropertyCrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Price",
                                            Crm = "sl_price",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn(string.Empty, true),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields: new(
                                    new RuleCrmField("sl_url"),
                                    new RuleCrmField("sl_pictureid"),
                                    new RuleCrmField("createdon"),
                                    new RuleCrmField("modifiedon")),
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Sync,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("sl_pictureid"),
                                            sqlName: "CrmId",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: true),
                                        new RuleField(
                                            crmField: new("createdon"),
                                            sqlName: "CrmCreationTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("modifiedon"),
                                            sqlName: "CrmModifiedTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures \n    ",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Join"
                                })
                        },
                        new EntityYaml
                        {
                            Name = "sl_listing",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Listing",
                                    Key = "AnotherCrmId",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Price",
                                            Crm = "sl_price",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn(string.Empty, false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields: new(
                                    new RuleCrmField("sl_url")),
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))),
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_listing",
                                entityPluralName: "sl_listings",
                                entityKeyFieldName: "sl_listingid",
                                fields: new(
                                    new RuleCrmField("sl_price")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Listing",
                                    keyFieldName: "AnotherCrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_price"),
                                            sqlName: "Price",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Join"
                                })
                        },
                        new EntityYaml
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Area",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn("sl_area", false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields: new(
                                    new RuleCrmField("sl_name")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("sl_area", "sl_picture")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Picture",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }),
                                    Operation = "Join"
                                })
                        },
                        new EntityYaml
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Area",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }),
                                    Operation = "Join"
                                })
                        },
                        new EntityYaml
                        {
                            Name = "sl_city",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "City",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }),
                                    Operation = "Join"
                                })
                        })
                },
                new RuleSetGetIn(null, false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields: new(
                                    new RuleCrmField("sl_url")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))),
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields: new(
                                    new RuleCrmField("sl_name")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            },
            new object[]
            {
                new RuleSetGetOption("./configuration.yaml", new("*")),
                new ConfigurationYaml
                {
                    Entities = new FlatArray<EntityYaml>(
                        new EntityYaml
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables = new FlatArray<TableYaml>(
                                new TableYaml
                                {
                                    Name = "Area",
                                    Fields = new FlatArray<FieldYaml>(
                                        new FieldYaml
                                        {
                                            Type = "String",
                                            LookupName = "sl_country \n\n ",
                                            Sql = "Name \n\n",
                                            Crm = "sl_name",
                                            Map = new()
                                            {
                                                { "000211", "1" },
                                                { "000212", "2" }
                                            },
                                            SkipNullable = true,
                                        },
                                        new FieldYaml
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername",
                                        },
                                        new FieldYaml
                                        {
                                            Type = "Int32",
                                            Sql = "CityCount",
                                            Crm = "sl_citycount",
                                        },
                                        new FieldYaml
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization",
                                        },
                                        new FieldYaml
                                        {
                                            Type = "Boolean",
                                            Sql = "IsCapital",
                                            Crm = "sl_capitalbit",
                                        }),
                                    Operation = "Join",
                                }),
                        })
                },
                new RuleSetGetIn(null, false),
                new RuleSetGetOut(new(
                        new RuleEntity(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields: new(
                                    new RuleCrmField("sl_name", "sl_country"),
                                    new RuleCrmField("sl_lowername"),
                                    new RuleCrmField("sl_citycount"),
                                    new RuleCrmField("sl_capitalization"),
                                    new RuleCrmField("sl_capitalbit")),
                                filter: null,
                                includeAnnotations: null
                            ),
                            tables: new(
                                new RuleTable(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields: new(
                                        new RuleField(
                                            crmField: new("sl_name", "sl_country"),
                                            sqlName: "Name",
                                            type: RuleFieldType.String,
                                            matcherRule: new(
                                                new KeyValuePair<string, string?>("000211", "1"),
                                                new KeyValuePair<string, string?>("000212", "2")),
                                            skipNullable: true),
                                        new RuleField(
                                            crmField: new("sl_lowername"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("sl_citycount"),
                                            sqlName: "CityCount",
                                            type: RuleFieldType.Int32,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("sl_capitalization"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new RuleField(
                                            crmField: new("sl_capitalbit"),
                                            sqlName: "IsCapital",
                                            type: RuleFieldType.Boolean,
                                            matcherRule: default,
                                            skipNullable: false))))))),
            }
        };
}