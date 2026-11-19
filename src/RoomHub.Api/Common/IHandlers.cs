namespace RoomHub.Api.Common;

// Command that returns data (for example the id of what was created).
public interface ICommandHandler<in TCommand, TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct);
}

// Command that only succeeds or fails.
public interface ICommandHandler<in TCommand>
{
    Task<Result> Handle(TCommand command, CancellationToken ct);
}

// Query: reads data, changes nothing.
public interface IQueryHandler<in TQuery, TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct);
}
