using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class RuleSetGetTestSource
{
    public static TheoryData<RuleSetGetOption, ConfigurationYaml, RuleSetGetIn, RuleSetGetOut> InputTestData
        =>
        new()
        {
            {
                new("./configuration.yaml", default),
                default,
                new(string.Empty, false),
                default
            },
            {
                new("./configuration.yaml", default),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Key = "Id",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Update"
                                }
                            ]
                        }
                    ]
                },
                new(string.Empty, false),
                default
            },
            {
                new(
                    "./configuration.yaml",
                    new("sl_pictures", "sl_unit")),
                new(),
                new(string.Empty, false),
                new()
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Key = "CrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Sync"
                                }
                            ]
                        },
                        new()
                        {
                            Name = "sl_unit",
                            Plural = "sl_units",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Property",
                                    Key = "CrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new(string.Empty, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields:
                                [
                                    new("sl_url"),
                                    new("sl_pictureid"),
                                    new("createdon"),
                                    new("modifiedon")
                                ],
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Sync,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_pictureid"),
                                            sqlName: "CrmId",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: true),
                                        new(
                                            crmField: new("createdon"),
                                            sqlName: "CrmCreationTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("modifiedon"),
                                            sqlName: "CrmModifiedTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ]),
                        new(
                            crmData: new(
                                entityName: "sl_unit",
                                entityPluralName: "sl_units",
                                entityKeyFieldName: "sl_unitid",
                                fields:
                                [
                                    new("sl_name")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Property",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_unit  \n\n",
                            Plural = "sl_units",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Property",
                                    Key = "CrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new(string.Empty, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_unit",
                                entityPluralName: "sl_units",
                                entityKeyFieldName: "sl_unitid",
                                fields:
                                [
                                    new("sl_name")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Property",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Filter = "sl_id ne null \n\n   \n",
                            Annotations = "OData.IncludeAnnotations    \n\n",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Key = "CrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Sync"
                                },
                                new()
                                {
                                    Name = "Listing",
                                    Key = "PropertyCrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Price",
                                            Crm = "sl_price",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new(string.Empty, true),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields:
                                [
                                    new("sl_url"),
                                    new("sl_pictureid"),
                                    new("createdon"),
                                    new("modifiedon")
                                ],
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Sync,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_pictureid"),
                                            sqlName: "CrmId",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: true),
                                        new(
                                            crmField: new("createdon"),
                                            sqlName: "CrmCreationTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("modifiedon"),
                                            sqlName: "CrmModifiedTime",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures \n    ",
                            Filter = "sl_id ne null",
                            Annotations = "OData.IncludeAnnotations",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        },
                        new()
                        {
                            Name = "sl_listing",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Listing",
                                    Key = "AnotherCrmId",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Price",
                                            Crm = "sl_price",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new(string.Empty, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields:
                                [
                                    new("sl_url")
                                ],
                                filter: "sl_id ne null",
                                includeAnnotations: "OData.IncludeAnnotations"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ]),
                        new(
                            crmData: new(
                                entityName: "sl_listing",
                                entityPluralName: "sl_listings",
                                entityKeyFieldName: "sl_listingid",
                                fields:
                                [
                                    new("sl_price")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Listing",
                                    keyFieldName: "AnotherCrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_price"),
                                            sqlName: "Price",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        },
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new("sl_area", false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new("sl_name")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("sl_area", "sl_picture")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_picture",
                            Plural = "sl_pictures",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Picture",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Url",
                                            Crm = "sl_url",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        },
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        },
                        new()
                        {
                            Name = "sl_city",
                            Tables =
                            [
                                new()
                                {
                                    Name = "City",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Sql = "Name",
                                            Crm = "sl_name",
                                        }
                                    ],
                                    Operation = "Join"
                                }
                            ]
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_picture",
                                entityPluralName: "sl_pictures",
                                entityKeyFieldName: "sl_pictureid",
                                fields:
                                [
                                    new("sl_url")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Picture",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_url"),
                                            sqlName: "Url",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ]),
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new("sl_name")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name"),
                                            sqlName: "Name",
                                            type: RuleFieldType.Default,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
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
                                        new()
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername",
                                        },
                                        new()
                                        {
                                            Type = "Int32",
                                            Sql = "CityCount",
                                            Crm = "sl_citycount",
                                        },
                                        new()
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization",
                                        },
                                        new()
                                        {
                                            Type = "Boolean",
                                            Sql = "IsCapital",
                                            Crm = "sl_capitalbit",
                                        }
                                    ],
                                    Operation = "Join",
                                }
                            ],
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new("sl_name", "sl_country"),
                                    new("sl_lowername"),
                                    new("sl_citycount"),
                                    new("sl_capitalization"),
                                    new("sl_capitalbit")
                                ],
                                filter: null,
                                includeAnnotations: null),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name", "sl_country"),
                                            sqlName: "Name",
                                            type: RuleFieldType.String,
                                            matcherRule: new(
                                                new KeyValuePair<string, string?>("000211", "1"),
                                                new KeyValuePair<string, string?>("000212", "2")),
                                            skipNullable: true),
                                        new(
                                            crmField: new("sl_lowername"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_citycount"),
                                            sqlName: "CityCount",
                                            type: RuleFieldType.Int32,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalization"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalbit"),
                                            sqlName: "IsCapital",
                                            type: RuleFieldType.Boolean,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Type = "String",
                                            LookupName = "sl_country@ForamtedValue \n\n ",
                                            Sql = "Name \n\n",
                                            Crm = "sl_name",
                                            Map = new()
                                            {
                                                { "000211", "1" },
                                                { "000212", "2" }
                                            },
                                            SkipNullable = true,
                                        },
                                        new()
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername@Entry.*",
                                        },
                                        new()
                                        {
                                            Type = "Int32",
                                            Sql = "CityCount",
                                            Crm = "sl_citycount",
                                        },
                                        new()
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization@ForamtedValue",
                                        },
                                        new()
                                        {
                                            Type = "Boolean",
                                            Sql = "IsCapital",
                                            Crm = "sl_capitalbit@Entry.*",
                                        }
                                    ],
                                    Operation = "Join",
                                }
                            ],
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new("sl_name", "sl_country@ForamtedValue"),
                                    new("sl_lowername@Entry.*"),
                                    new("sl_citycount"),
                                    new("sl_capitalization@ForamtedValue"),
                                    new("sl_capitalbit@Entry.*")
                                ],
                                filter: null,
                                includeAnnotations: "ForamtedValue,Entry.*"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name", "sl_country@ForamtedValue"),
                                            sqlName: "Name",
                                            type: RuleFieldType.String,
                                            matcherRule: new KeyValuePair<string, string?>[]
                                            {
                                                new("000211", "1"),
                                                new("000212", "2")
                                            },
                                            skipNullable: true),
                                        new(
                                            crmField: new("sl_lowername@Entry.*"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_citycount"),
                                            sqlName: "CityCount",
                                            type: RuleFieldType.Int32,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalization@ForamtedValue"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalbit@Entry.*"),
                                            sqlName: "IsCapital",
                                            type: RuleFieldType.Boolean,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Type = "String",
                                            LookupName = "sl_country@Microsoft.Dynamics.CRM.associatednavigationproperty \n\n ",
                                            Sql = "Name \n\n",
                                            Crm = "sl_name",
                                            Map = new()
                                            {
                                                { "000211", "1" },
                                                { "000212", "2" }
                                            },
                                            SkipNullable = true,
                                        },
                                        new()
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                        },
                                        new()
                                        {
                                            Type = "Int32",
                                            Sql = "CityCount",
                                            Crm = "sl_citycount@Microsoft.Dynamics.CRM.lookuplogicalname",
                                        },
                                        new()
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization@OData.Community.Display.V1.FormattedValue",
                                        },
                                        new()
                                        {
                                            Type = "Boolean",
                                            Sql = "IsCapital",
                                            Crm = "sl_capitalbit",
                                        }
                                    ],
                                    Operation = "Join",
                                }
                            ],
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new("sl_name", "sl_country@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                    new("sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname"),
                                    new("sl_citycount@Microsoft.Dynamics.CRM.lookuplogicalname"),
                                    new("sl_capitalization@OData.Community.Display.V1.FormattedValue"),
                                    new("sl_capitalbit")
                                ],
                                filter: null,
                                includeAnnotations: "Microsoft.Dynamics.CRM.associatednavigationproperty,Microsoft.Dynamics.CRM.lookuplogicalname," +
                                                    "OData.Community.Display.V1.FormattedValue"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new("sl_name", "sl_country@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                            sqlName: "Name",
                                            type: RuleFieldType.String,
                                            matcherRule: new KeyValuePair<string, string?>[]
                                            {
                                                new("000211", "1"),
                                                new("000212", "2")
                                            },
                                            skipNullable: true),
                                        new(
                                            crmField: new("sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_citycount@Microsoft.Dynamics.CRM.lookuplogicalname"),
                                            sqlName: "CityCount",
                                            type: RuleFieldType.Int32,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalization@OData.Community.Display.V1.FormattedValue"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new("sl_capitalbit"),
                                            sqlName: "IsCapital",
                                            type: RuleFieldType.Boolean,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                            LookupName = "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"
                                        },
                                        new()
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization@OData.Community.Display.V1.FormattedValue",
                                        }
                                    ],
                                    Operation = "Join",
                                }
                            ],
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new(
                                        "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                        "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                    new(
                                        "sl_capitalization@OData.Community.Display.V1.FormattedValue")
                                ],
                                filter: null,
                                includeAnnotations: "Microsoft.Dynamics.CRM.lookuplogicalname,Microsoft.Dynamics.CRM.associatednavigationproperty," +
                                                    "OData.Community.Display.V1.FormattedValue"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new(
                                                "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                                "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new(
                                                "sl_capitalization@OData.Community.Display.V1.FormattedValue"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            },
            {
                new("./configuration.yaml", new("*")),
                new()
                {
                    Entities =
                    [
                        new()
                        {
                            Name = "sl_area",
                            Plural = "sl_areas",
                            Annotations = "PrimmaryAnnotationSholdBeChosen",
                            Tables =
                            [
                                new()
                                {
                                    Name = "Area",
                                    Fields =
                                    [
                                        new()
                                        {
                                            Type = "LowerString",
                                            Sql = "LowerCaseName",
                                            Crm = "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                            LookupName = "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"
                                        },
                                        new()
                                        {
                                            Type = "Decimal",
                                            Sql = "CityCapitalization",
                                            Crm = "sl_capitalization@OData.Community.Display.V1.FormattedValue",
                                        }
                                    ],
                                    Operation = "Join",
                                }
                            ],
                        }
                    ]
                },
                new(null, false),
                new(
                    entities:
                    [
                        new(
                            crmData: new(
                                entityName: "sl_area",
                                entityPluralName: "sl_areas",
                                entityKeyFieldName: "sl_areaid",
                                fields:
                                [
                                    new(
                                        "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                        "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                    new(
                                        "sl_capitalization@OData.Community.Display.V1.FormattedValue")
                                ],
                                filter: null,
                                includeAnnotations: "PrimmaryAnnotationSholdBeChosen"),
                            tables:
                            [
                                new(
                                    operation: RuleItemOperation.Join,
                                    tableName: "Area",
                                    keyFieldName: "CrmId",
                                    fields:
                                    [
                                        new(
                                            crmField: new(
                                                "sl_lowername@Microsoft.Dynamics.CRM.lookuplogicalname",
                                                "sl_lowernamelookup@Microsoft.Dynamics.CRM.associatednavigationproperty"),
                                            sqlName: "LowerCaseName",
                                            type: RuleFieldType.LowerString,
                                            matcherRule: default,
                                            skipNullable: false),
                                        new(
                                            crmField: new(
                                                "sl_capitalization@OData.Community.Display.V1.FormattedValue"),
                                            sqlName: "CityCapitalization",
                                            type: RuleFieldType.Decimal,
                                            matcherRule: default,
                                            skipNullable: false)
                                    ])
                            ])
                    ])
            }
        };
}