using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public ValueTask<Result<Unit, Failure<WriteAuditDateTimeFailureCode>>> WriteAuditDateTimeAsync(
        WriteAuditDateTimeIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input, cancellationToken)
        .HandleCancellation()
        .Pipe(
            ValidateInput)
        .MapSuccess(
            CreateAuditDateDbQuery)
        .ForwardValue(
            sqlExecuteNonQueryApi.ExecuteNonQueryOrFailureAsync,
            failure => failure.MapFailureCode(MapUnknownFailureCode))
        .MapSuccess(
            success => Unit.From(success));

    private static Result<WriteAuditDateTimeIn, Failure<WriteAuditDateTimeFailureCode>> ValidateInput(WriteAuditDateTimeIn input)
        =>
        string.IsNullOrWhiteSpace(input.EntityName) switch
        {
            false => input,
            _ => Failure.Create(WriteAuditDateTimeFailureCode.EntityMustBeSpecified, "Entity name must be specified")
        };

    private static DbUpdateQuery CreateAuditDateDbQuery(WriteAuditDateTimeIn input)
        =>
        new(
            tableName: AuditDateTimeTableName,
            fieldValues: new(
                new(nameof(input.EntityName), input.EntityName),
                new(nameof(input.AuditDateTime), input.AuditDateTime)),
            filter: new DbParameterFilter(nameof(input.EntityName), DbFilterOperator.Equal, input.EntityName));

    private static WriteAuditDateTimeFailureCode MapUnknownFailureCode(Unit _)
        =>
        WriteAuditDateTimeFailureCode.Unknown;
}