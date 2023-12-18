using GarageGroup.Infra;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class Application
{
    [HttpFunction("HealthCheck", HttpMethodName.Get, Route = "health", AuthLevel = HttpAuthorizationLevel.Function)]
    internal static Dependency<IHealthCheckHandler> UseHealthCheck()
        =>
        HealthCheck.UseServices(
            UseDataverseApi().UseServiceHealthCheckApi("DataverseApi"),
            UseSqlApi().UseServiceHealthCheckApi("BlobStorage"))
        .UseHealthCheckHandler();
}