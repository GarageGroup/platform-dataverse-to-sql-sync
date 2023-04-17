using GGroupp.Infra;
using PrimeFuncPack;

namespace GarageGroup.Platform.DataMover;

partial class Application
{
    public static Dependency<SqlMigrateHandler> UseSqlMigrateHandler()
        =>
        UseSqlApi().UseSqlMigrateHandler("Migrations:ConfigPath");
}