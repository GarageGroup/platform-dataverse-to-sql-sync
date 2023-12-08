using System;
using System.Collections.Generic;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

partial class CrmEntityFlowGetFuncSource
{
    public static IEnumerable<object[]> InputTestData
        =>
        new object[][]
        {
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_unit",
                    pluralName: "sl_units",
                    fields: new("sl_name", "sl_description", "sl_dishwasherbit"),
                    lookups: default,
                    filter: null,
                    pageSize: 20,
                    includeAnnotations: null),
                new DataverseEntitySetGetOut<CrmEntityJson>(
                    value: default,
                    nextLink: null),
                default(FlatArray<CrmEntitySet>)
            ],
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_unit",
                    pluralName: "sl_units",
                    fields: new("sl_name", "sl_description", "sl_dishwasherbit"),
                    lookups: default,
                    filter: null,
                    pageSize: 20,
                    includeAnnotations: null),
                new DataverseEntitySetGetOut<CrmEntityJson>(
                    value: new CrmEntityJson[]
                    {
                        new()
                        {
                            ExtensionData = new()
                            {
                                { "sl_unitid", GetJsonElement("e4195a96-8689-ec11-93b0-6045bd91d810") },
                                { "sl_name", GetJsonElement("Penthouse 211") },
                                { "sl_description", GetJsonElement("Near the sea and so impressive property") },
                                { "sl_dishwasherbit", GetJsonElement(true) }
                            }
                        }
                    },
                    nextLink: null),
                new CrmEntitySet[]
                {
                    new(
                        entities: new CrmEntity[]
                        {
                            new(
                                id: Guid.Parse("e4195a96-8689-ec11-93b0-6045bd91d810"),
                                fieldValues: new CrmEntityFieldValue[]
                                {
                                    new("sl_unitid", GetJsonElement("e4195a96-8689-ec11-93b0-6045bd91d810")),
                                    new("sl_name", GetJsonElement("Penthouse 211")),
                                    new("sl_description", GetJsonElement("Near the sea and so impressive property")),
                                    new("sl_dishwasherbit", GetJsonElement(true))
                                })
                        })
                }
            ],
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_project",
                    pluralName: "sl_projects",
                    fields: new("sl_name", "sl_completiondate", "sl_startdate", "sl_picture"),
                    lookups: new CrmEntityLookup[]
                    {
                        new("sl_picture", "sl_pictureid/sl_url")
                    },
                    filter: null,
                    pageSize: 20,
                    includeAnnotations: null),
                new DataverseEntitySetGetOut<CrmEntityJson>(
                    value: new CrmEntityJson[]
                    {
                        new()
                        {
                            ExtensionData = new()
                            {
                                { "sl_projectid", GetJsonElement("57372b46-9197-4201-88ab-be3316b5a880") },
                                { "sl_name", GetJsonElement("Starline Valley") },
                                { "sl_completiondate", GetJsonElement(new DateTime(2021, 5, 3)) },
                                { "sl_startdate", GetJsonElement(new DateTime(2020, 5, 3)) },
                                { "sl_picture", GetJsonElement("https://ilove-unit-testing.io/xunit") }
                            }
                        },
                        new()
                        {
                            ExtensionData = new()
                            {
                                { "sl_projectid", GetJsonElement("ddb3a550-b083-4adb-b2aa-6643e34c726e") },
                                { "sl_name", GetJsonElement("Moonline Valley") },
                                { "sl_completiondate", GetJsonElement(new DateTime(2023, 2, 1)) },
                                { "sl_startdate", GetJsonElement(new DateTime(2022, 9, 20)) },
                                { "sl_picture", GetJsonElement("https://test.com") }
                            }
                        }
                    },
                    nextLink: null),
                new CrmEntitySet[]
                {
                    new(
                        entities: new CrmEntity[]
                        {
                            new(
                                id: Guid.Parse("57372b46-9197-4201-88ab-be3316b5a880"),
                                fieldValues: new CrmEntityFieldValue[]
                                {
                                    new("sl_projectid", GetJsonElement("57372b46-9197-4201-88ab-be3316b5a880")),
                                    new("sl_name", GetJsonElement("Starline Valley")),
                                    new("sl_completiondate", GetJsonElement(new DateTime(2021, 5, 3))),
                                    new("sl_startdate", GetJsonElement(new DateTime(2020, 5, 3))),
                                    new("sl_picture", GetJsonElement("https://ilove-unit-testing.io/xunit"))
                                }),
                            new(
                                id: Guid.Parse("ddb3a550-b083-4adb-b2aa-6643e34c726e"),
                                fieldValues: new CrmEntityFieldValue[]
                                {
                                    new("sl_projectid", GetJsonElement("ddb3a550-b083-4adb-b2aa-6643e34c726e")),
                                    new("sl_name", GetJsonElement("Moonline Valley")),
                                    new("sl_completiondate", GetJsonElement(new DateTime(2023, 2, 1))),
                                    new("sl_startdate", GetJsonElement(new DateTime(2022, 9, 20))),
                                    new("sl_picture", GetJsonElement("https://test.com"))
                                })
                        })
                }
            ]
        };

    public static IEnumerable<object[]> DataverseInputTestData
        =>
        new object [][]
        {
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_project",
                    pluralName: "sl_projects",
                    fields: new("sl_name", "sl_completiondate", "sl_startdate", "sl_picture"),
                    lookups: new CrmEntityLookup[]
                    {
                        new("sl_picture", "sl_pictureid/sl_url")
                    },
                    filter: null,
                    pageSize: 20,
                    includeAnnotations: null),
                new DataverseEntitySetGetOut<CrmEntityJson>[]
                {
                    new(
                        value: new CrmEntityJson[]
                        {
                            new()
                            {
                                ExtensionData = new()
                                {
                                    { "sl_projectid", GetJsonElement("0ae48926-d064-4a2f-b79b-47f31aea60a3") },
                                    { "sl_name", GetJsonElement("Another project") },
                                    { "sl_completiondate", GetJsonElement("23.04.2023") },
                                    { "sl_startdate", GetJsonElement("03.01.2020") },
                                    { "sl_picture", GetJsonElement("https://bad-pictures.com") },
                                }
                            }
                        },
                        nextLink: "link2"),
                    new(
                        value: new CrmEntityJson[]
                        {
                            new()
                            {
                                ExtensionData = new()
                                {
                                    { "sl_projectid", GetJsonElement("e68d62ba-4117-4c9a-a610-5e4a92f527bf") },
                                    { "sl_name", GetJsonElement("Test project") },
                                    { "sl_completiondate", GetJsonElement("10.02.2022") },
                                    { "sl_startdate", GetJsonElement("09.02.2022") },
                                    { "sl_picture", GetJsonElement("https://good-pictures.com") },
                                }
                            }
                        })
                },
                new DataverseEntitySetGetIn[]
                {
                    new(
                        entityPluralName: "sl_projects",
                        selectFields: new("sl_name", "sl_completiondate", "sl_startdate", "sl_picture"),
                        expandFields: new(
                            new DataverseExpandedField(
                                fieldName: "sl_picture",
                                selectFields: new("sl_pictureid/sl_url"))),
                        filter: string.Empty)
                    {
                        MaxPageSize = 20,
                        IncludeAnnotations = null,
                    },
                    new(nextLink: "link2")
                    {
                        MaxPageSize = 20,
                    }
                },
            ],
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_unit",
                    pluralName: "sl_units",
                    fields: new("sl_name"),
                    lookups: default,
                    filter: "sl_id ne null",
                    pageSize: 10,
                    includeAnnotations: "OData annotations"),
                new DataverseEntitySetGetOut<CrmEntityJson>[]
                {
                    new(
                        value: new CrmEntityJson[]
                        {
                            new()
                            {
                                ExtensionData = new()
                                {
                                    { "sl_unitid", GetJsonElement("b3b0c919-26cd-4b76-b71f-de9d2796aafb") },
                                    { "sl_name", GetJsonElement("Property") }
                                }
                            }
                        })
                },
                new DataverseEntitySetGetIn[]
                {
                    new(
                        entityPluralName: "sl_units",
                        selectFields: new("sl_name"),
                        expandFields: default,
                        filter: "sl_id ne null")
                    {
                        MaxPageSize = 10,
                        IncludeAnnotations = "OData annotations",
                    }
                }
            ],
            [
                new CrmEntityFlowGetIn(
                    entityName: "sl_unit",
                    pluralName: "sl_units",
                    fields: new("sl_name", "sl_name", "sl_description", "sl_name"),
                    lookups: new CrmEntityLookup[]
                    {
                        new("sl_project", "sl_name"),
                        new("sl_project", "sl_name"),
                        new("sl_project", "sl_description")
                    },
                    filter: string.Empty,
                    pageSize: 10,
                    includeAnnotations: null),
                new DataverseEntitySetGetOut<CrmEntityJson>[]
                {
                    new(
                        value: new CrmEntityJson[]
                        {
                            new()
                            {
                                ExtensionData = new()
                                {
                                    { "sl_unitid", GetJsonElement("19e22188-c884-11ed-afa1-0242ac120002") },
                                    { "sl_name", GetJsonElement("Project name") },
                                    { "sl_description", GetJsonElement("Project description") }
                                }
                            }
                        })
                },
                new DataverseEntitySetGetIn[]
                {
                    new(
                        entityPluralName: "sl_units",
                        selectFields: new("sl_name", "sl_description"),
                        expandFields: new DataverseExpandedField[]
                        {
                            new("sl_project", new("sl_name", "sl_description"))
                        },
                        filter: string.Empty)
                    {
                        MaxPageSize = 10,
                    }
                }
            ]
        };
}