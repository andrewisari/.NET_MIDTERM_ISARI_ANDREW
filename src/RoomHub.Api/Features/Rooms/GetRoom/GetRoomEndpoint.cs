using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public static class GetRoomEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("/api/rooms/{id:guid}", async (
            Guid id,
            IQueryHandler<GetRoomQuery, RoomResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(new GetRoomQuery(id), ct);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblem();
        });
}
