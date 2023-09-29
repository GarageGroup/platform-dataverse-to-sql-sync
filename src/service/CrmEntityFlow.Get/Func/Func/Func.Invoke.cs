using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class CrmEntityFlowGetFunc
{
    public async IAsyncEnumerable<CrmEntitySet> InvokeAsync(
        CrmEntityFlowGetIn input, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        var firstRequest = new DataverseEntitySetGetIn(
            entityPluralName: input.PluralName,
            selectFields: input.Fields.AsEnumerable().Distinct().ToFlatArray(),
            expandFields: input.Lookups.AsEnumerable().GroupBy(GetRootFieldName).Select(ToExpandedField).Distinct().ToFlatArray(),
            filter: input.Filter)
        {
            IncludeAnnotations = input.IncludeAnnotations,
            MaxPageSize = input.PageSize
        };

        var response = await GetEntitiesAsync(firstRequest, cancellationToken).ConfigureAwait(false);

        while (true)
        {
            yield return new(response.Value.Map(MapEntityJson));

            if (string.IsNullOrEmpty(response.NextLink))
            {
                break;
            }

            var nextRequest = new DataverseEntitySetGetIn(response.NextLink)
            {
                IncludeAnnotations = input.IncludeAnnotations,
                MaxPageSize = input.PageSize
            };

            response = await GetEntitiesAsync(nextRequest, cancellationToken).ConfigureAwait(false);
        }

        CrmEntity MapEntityJson(CrmEntityJson entityJson)
            =>
            CreateCrmEntity(input.EntityName, entityJson);

        static DataverseExpandedField ToExpandedField(IGrouping<string, CrmEntityLookup> lookupGroup)
            =>
            new(lookupGroup.Key, lookupGroup.Select(GetLookupFieldName).Distinct().ToFlatArray());

        static string GetRootFieldName(CrmEntityLookup lookup)
            =>
            lookup.RootFieldName;

        static string GetLookupFieldName(CrmEntityLookup lookup)
            =>
            lookup.LookupFieldName.Split('@')[0];
    }

    private async ValueTask<DataverseEntitySetGetOut<CrmEntityJson>> GetEntitiesAsync(
        DataverseEntitySetGetIn input, CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                var result = await dataverseApiClient.GetEntitySetAsync<CrmEntityJson>(input, cancellationToken).ConfigureAwait(false);
                return result.SuccessOrThrow(CreateUnsuccessfulResponseException);
            }
            catch (TaskCanceledException)
                when (cancellationToken.IsCancellationRequested is false)
            {
                // repeat till token is canceled
            }
        }

        static InvalidOperationException CreateUnsuccessfulResponseException(Failure<DataverseFailureCode> failure)
            =>
            new($"The response was unsuccessful: {failure.FailureCode}. {failure.FailureMessage}");
    }

    private static CrmEntity CreateCrmEntity(string entityName, CrmEntityJson entityJson)
    {
        var fieldValues = entityJson.ExtensionData;

        if (fieldValues?.Count is not > 0)
        {
            throw new InvalidOperationException("Field values response must be not empty");
        }

        return new(
            id: fieldValues[entityName + "id"].GetGuid(),
            fieldValues: fieldValues.Select(CreateFieldValue).ToFlatArray());

        static CrmEntityFieldValue CreateFieldValue(KeyValuePair<string, JsonElement> field)
            =>
            new(field.Key, field.Value);
    }
}