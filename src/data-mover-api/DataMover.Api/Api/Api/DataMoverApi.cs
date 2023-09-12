using Microsoft.Extensions.Logging;

namespace GarageGroup.Platform.DataMover;

internal sealed partial class DataMoverApi : IDataMoverApi
{
    private readonly IRuleSetGetFunc ruleSetGetFunc;

    private readonly IEntityMoveFunc entityMoveFunc;

    private readonly ILogger? logger;

    internal DataMoverApi(IRuleSetGetFunc ruleSetGetFunc, IEntityMoveFunc entityMoveFunc, ILoggerFactory? loggerFactory)
    {
        this.ruleSetGetFunc = ruleSetGetFunc;
        this.entityMoveFunc = entityMoveFunc;
        logger = loggerFactory?.CreateLogger<DataMoverApi>();
    }
}