using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;
using Moq;

namespace GarageGroup.Platform.DataverseToSqlSync.Test;

public static partial class CrmEntityFlowGetFuncTest
{
    private static DataverseEntitySetGetOut<CrmEntityJson> SomeDataverseEntitySetGetOut
        =>
        new(
            value: new(
                new CrmEntityJson
                {
                    ExtensionData = new Dictionary<string, JsonElement>
                    {
                        {

                            "sl_pictureid",
                            CrmEntityFlowGetFuncSource.GetJsonElement("67f9f94d-5327-43a3-85ad-22fa6fcb2e7b")
                        }
                    }
                }));

    private static CrmEntityFlowGetIn SomeCrmEntityFlowGetIn
        =>
        new(
            entityName: "sl_picture",
            pluralName: "sl_pictures",
            fields: new("sl_url"),
            lookups: new(),
            filter: null,
            pageSize: 20,
            includeAnnotations: null);

    private static Mock<IDataverseEntitySetGetSupplier> CreateMockDataverseApi(
        Result<DataverseEntitySetGetOut<CrmEntityJson>, Failure<DataverseFailureCode>> dataverseEntitySetGetOut)
    {
        var mock = new Mock<IDataverseEntitySetGetSupplier>();

        _ = mock
            .Setup(
                func => func.GetEntitySetAsync<CrmEntityJson>(It.IsAny<DataverseEntitySetGetIn>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dataverseEntitySetGetOut);

        return mock;
    }

    private static Mock<IDataverseEntitySetGetSupplier> CreateMockDataverseApi(
        DataverseEntitySetGetOut<CrmEntityJson>[] dataverseEntitySetGetOut)
    {
        var queue = new Queue<DataverseEntitySetGetOut<CrmEntityJson>>(dataverseEntitySetGetOut);
        var mock = new Mock<IDataverseEntitySetGetSupplier>();
        _ = mock
            .Setup(
                func => func.GetEntitySetAsync<CrmEntityJson>(It.IsAny<DataverseEntitySetGetIn>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => queue.Dequeue());

        return mock;
    }

    private static async Task<FlatArray<CrmEntitySet>> IterateOverAsyncEnumerableAsync(
        CrmEntityFlowGetFunc func, CrmEntityFlowGetIn input, CancellationToken cancellationToken)
    {
        var crmEntities = new List<CrmEntitySet>();
        await foreach (var crmEntitySet in func.InvokeAsync(input, cancellationToken).ConfigureAwait(false))
        {
            crmEntities.Add(crmEntitySet);
        }

        return crmEntities;
    }

    private static bool CompareCrmEntitySets(FlatArray<CrmEntitySet> first, FlatArray<CrmEntitySet> second)
    {
        if (first.Length != second.Length)
        {
            return false;
        }

        for (var i = 0; i < first.Length; i++)
        {
            if (first[i].Entities.Length != second[i].Entities.Length)
            {
                return false;
            }

            for (var j = 0; j < first[i].Entities.Length; j++)
            {
                if (first[i].Entities[j].Id != second[i].Entities[j].Id)
                {
                    return false;
                }

                for (var k = 0; k < first[i].Entities[j].FieldValues.Length; k++)
                {
                    if (IsEqual(first[i].Entities[j].FieldValues[k], second[i].Entities[j].FieldValues[k]) is false)
                    {
                        return false;
                    }
                }
            }
        }

        return true;

        static bool IsEqual(CrmEntityFieldValue first, CrmEntityFieldValue second)
            =>
            first.Name == second.Name &&
            string.Equals(first.JsonValue.ToString(), second.JsonValue.ToString());
    }
}