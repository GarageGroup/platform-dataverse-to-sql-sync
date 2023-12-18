using GarageGroup.Infra;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

internal static partial class Application
{
    private static Dependency<IDataverseApiClient> UseDataverseApi()
        =>
        PrimaryHandler.UseStandardSocketsHttpHandler()
        .UseLogging("DataverseApiClient")
        .UseTokenCredentialStandard()
        .UsePollyStandard()
        .UseDataverseApiClient();

    private static Dependency<ISqlApi> UseSqlApi()
        =>
        MicrosoftDbProvider.Configure("SqlDb")
        .UseSqlApi();
}