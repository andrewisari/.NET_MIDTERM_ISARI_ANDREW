using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(RoomRepository rooms) : IQueryHandler<GetRoomQuery, RoomResponse>
{
    public Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken ct)
    {
        var room = rooms.Get(query.Id);
        if (room is null)
            return Task.FromResult<Result<RoomResponse>>(RoomErrors.NotFound(query.Id));

        return Task.FromResult<Result<RoomResponse>>(RoomResponse.From(room));
    }
}
