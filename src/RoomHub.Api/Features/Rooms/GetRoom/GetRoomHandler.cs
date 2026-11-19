using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(IRoomRepository rooms) : IQueryHandler<GetRoomQuery, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken ct)
    {
        var room = await rooms.GetByIdAsync(query.Id, ct);
        if (room is null) return RoomErrors.NotFound(query.Id);

        return RoomResponse.From(room);
    }
}
