using System;
using System.Threading.Tasks;
using GGroupp.Infra;

namespace GarageGroup.Platform.DataMover;

static class Program
{
    static Task Main(string[] args)
        =>
        Pipeline.Pipe(
            Application.UseSqlMigrateHandler())
        .With(
            Application.UseDataMoveHandler())
        .Join<SqlMigrateHandler, IDataMoveHandler, Unit>()
        .RunConsoleAsync(args);
}