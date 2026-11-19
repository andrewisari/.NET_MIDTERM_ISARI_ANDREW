using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Room.NotFound", $"The room '{id}' does not exist.");

    public static readonly Error NameRequired =
        Error.Validation("Room.NameRequired", "Room name is required.");

    public static readonly Error CapacityNotPositive =
        Error.Validation("Room.CapacityNotPositive", "Capacity must be positive.");
}
