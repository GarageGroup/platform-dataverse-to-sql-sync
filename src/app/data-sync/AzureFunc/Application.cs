using System;
using System.Net;
using GarageGroup.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataMover;

internal static class Application
{
    [EventGridFunction("OnRecordModified")]
    internal static Dependency<IDataSyncHandler> UseDataSyncHandler()
        =>
        PrimaryHandler.UseStandardSocketsHttpHandler()
        .UseLogging("DataverseApiClient")
        .UsePollyStandard(HttpStatusCode.TooManyRequests)
        .UseDataverseApiClient()
        .UseCrmEntityFlowGetFunc()
        .With(
            MicrosoftDbProvider.Configure("SqlDb").UseSqlApi().UseDbDataApi())
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