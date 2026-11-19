using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public static class CreateRoomEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("/api/rooms", async (
            CreateRoomCommand command,
            ICommandHandler<CreateRoomCommand, RoomResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);

            return result.IsSuccess
                ? Results.Created($"/api/rooms/{result.Value.Id}", result.Value)
                : result.ToProblem();
        });
}
