using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Rooms;

public sealed class Room
{
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

    // R1: a room has a non-empty name and a strictly positive capacity.
    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name)) return RoomErrors.NameRequired;
        if (capacity <= 0) return RoomErrors.CapacityNotPositive;

        return new Room(Guid.NewGuid(), name.Trim(), capacity);
    }
}
