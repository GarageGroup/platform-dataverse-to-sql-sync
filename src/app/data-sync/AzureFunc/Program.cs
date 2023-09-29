using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace GarageGroup.Platform.DataverseToSqlSync;

static class Program
{
    static Task Main()
        =>
        Host.CreateDefaultBuilder()
        .ConfigureFunctionsWorkerStandard()
        .Build()
        .RunAsync();
}