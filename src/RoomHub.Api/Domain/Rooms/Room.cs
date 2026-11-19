using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Rooms;

public sealed class Room
{
    public const int LargeRoomCapacity = 10;

    private Room() { } // for EF Core

    private Room(Guid id, string name, int capacity)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int Capacity { get; private set; }

    // R6 + R10: rooms with more than 10 seats can be booked for at most 2 hours, the others for 4 hours.
    public TimeSpan MaxBookingDuration =>
        Capacity > LargeRoomCapacity ? TimeSpan.FromHours(2) : TimeSpan.FromHours(4);

    // R1: a room has a non-empty name and a strictly positive capacity.
    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name)) return RoomErrors.NameRequired;
        if (capacity <= 0) return RoomErrors.CapacityNotPositive;

        return new Room(Guid.NewGuid(), name.Trim(), capacity);
    }
}
