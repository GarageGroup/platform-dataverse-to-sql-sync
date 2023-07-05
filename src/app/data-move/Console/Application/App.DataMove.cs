using System;
using System.Linq;
using System.Net;
using GarageGroup.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataMover;

partial class Application
{
    public static Dependency<IDataMoveHandler> UseDataMoveHandler()
        =>
        PrimaryHandler.UseStandardSocketsHttpHandler()
        .UseLogging("DataverseApiClient")
        .UsePollyStandard(HttpStatusCode.TooManyRequests)
        .UseDataverseApiClient()
        .UseCrmEntityFlowGetFunc()
        .With(
            UseSqlApi().UseDbDataApi())
        .With(
            Dependency.From(ResolveRuleSetGetOption).UseRuleSetGetFunc())
        .With(
            ResolveDataMoveOption)
        .UseDataMoverApi()
        .UseDataMoveHandler();

    private static RuleSetGetOption ResolveRuleSetGetOption(IServiceProvider serviceProvider)
    {
        var section = serviceProvider.GetRequiredService<IConfiguration>().GetRequiredSection("Rules");

        return new(
            configFilePath: section["ConfigPath"].OrEmpty(),
            entityNames: section["Entities"].ParseEntityNames());
    }

    private static DataMoveOption ResolveDataMoveOption(IServiceProvider serviceProvider)
        =>
        serviceProvider.GetRequiredService<IConfiguration>().GetSection("DataMove").Get<DataMoveOption>();

    private static FlatArray<string> ParseEntityNames(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return default;
        }

        return source.Split(',').Select(Trim).Where(NotEmpty).ToFlatArray();

        static string Trim(string value)
            =>
            value.Trim();

        static bool NotEmpty(string value)
            =>
            string.IsNullOrEmpty(value) is false;
    }
}