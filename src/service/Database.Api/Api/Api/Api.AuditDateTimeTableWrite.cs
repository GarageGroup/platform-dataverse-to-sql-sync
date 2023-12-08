using System;
using System.Threading;
using System.Threading.Tasks;
using GarageGroup.Infra;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class DatabaseApi
{
    public ValueTask<Result<Unit, Failure<Unit>>> WriteAuditDateTimeAsync(
        AuditDateTimeWriteIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Pipe(
            input, cancellationToken)
        .Pipe(
            ValidateInput)
        .MapSuccess(
            CreateAuditDateDbQuery)
        .ForwardValue(
            sqlApi.ExecuteNonQueryOrFailureAsync)
        .MapSuccess(
            Unit.From);

    private static Result<AuditDateTimeWriteIn, Failure<Unit>> ValidateInput(AuditDateTimeWriteIn input)
        =>
        string.IsNullOrWhiteSpace(input.EntityName) switch
        {
            false => input,
            _ => Failure.Create("Entity name must be specified")
        };

    private static DbUpdateQuery CreateAuditDateDbQuery(AuditDateTimeWriteIn input)
        =>
        new(
            tableName: AuditDateTimeTableName,
            fieldValues: new(
                new(nameof(input.EntityName), input.EntityName),
                new(nameof(input.AuditDateTime), input.AuditDateTime)),
            filter: new DbParameterFilter(nameof(input.EntityName), DbFilterOperator.Equal, input.EntityName));
}