using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Models;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(RoomRepository rooms) : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    public Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Task.FromResult<Result<RoomResponse>>(RoomErrors.NameRequired);

        if (command.Capacity <= 0)
            return Task.FromResult<Result<RoomResponse>>(RoomErrors.CapacityNotPositive);

        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Capacity = command.Capacity
        };

        rooms.Add(room);
        return Task.FromResult<Result<RoomResponse>>(RoomResponse.From(room));
    }
}
