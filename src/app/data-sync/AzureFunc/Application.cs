using System;
using GarageGroup.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal static class Application
{
    [ServiceBusFunction(
        "OnRecordModified",
        "%ServiceBus:TopicName%",
        "%ServiceBus:SubscriptionName%",
        "ServiceBus:ConnectionString")]
    internal static Dependency<IDataSyncHandler> UseDataSyncHandler()
        =>
        PrimaryHandler.UseStandardSocketsHttpHandler()
        .UseLogging("DataverseApiClient")
        .UseDataverseAzureCredentialStandard()
        .UsePollyStandard()
        .UseDataverseApiClient()
        .UseCrmEntityFlowGetFunc()
        .With(
            MicrosoftDbProvider.Configure("SqlDb").UseSqlApi().UseDatabaseApi())
        .With(
            Dependency.From(ResolveRuleSetGetOption).UseRuleSetGetFunc())
        .With(
            ResolveDataMoveOption)
        .UseDataMoverApi()
        .UseDataSyncHandler();

    private static RuleSetGetOption ResolveRuleSetGetOption(IServiceProvider serviceProvider)
    {
        var section = serviceProvider.GetRequiredService<IConfiguration>().GetRequiredSection("Rules");

        return new(
            configFilePath: section["ConfigPath"].OrEmpty(),
            entityNames: new("*"));
    }

    private static DataMoveOption ResolveDataMoveOption(IServiceProvider serviceProvider)
        =>
        serviceProvider.GetRequiredService<IConfiguration>().GetSection("DataMove").Get<DataMoveOption>();
}