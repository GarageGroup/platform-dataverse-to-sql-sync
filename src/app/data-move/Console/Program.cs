using System;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataMover;

static class Program
{
    static Task Main(string[] args)
            =>
            Application.UseDataMoveHandler()
            .RunConsoleAsync(args);
}
