using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(RoomRepository rooms) : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    public Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken ct)
    {
        var created = Room.Create(command.Name, command.Capacity);
        if (created.IsFailure)
            return Task.FromResult<Result<RoomResponse>>(created.Error);

        rooms.Add(created.Value);
        return Task.FromResult<Result<RoomResponse>>(RoomResponse.From(created.Value));
    }
}
