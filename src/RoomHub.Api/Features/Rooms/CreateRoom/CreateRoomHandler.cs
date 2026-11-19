using RoomHub.Api.Common;
using RoomHub.Api.Domain.Abstractions;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository rooms, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken ct)
    {
        var created = Room.Create(command.Name, command.Capacity);
        if (created.IsFailure) return created.Error;

        var room = created.Value;
        rooms.Add(room);
        await unitOfWork.SaveChangesAsync(ct);

        return RoomResponse.From(room);
    }
}
