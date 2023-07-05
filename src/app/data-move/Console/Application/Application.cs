using GarageGroup.Infra;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataMover;

internal static partial class Application
{
    private static Dependency<ISqlApi> UseSqlApi()
        =>
        MicrosoftDbProvider.Configure("SqlDb").UseSqlApi();
}