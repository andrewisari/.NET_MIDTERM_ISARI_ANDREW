using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Domain.Bookings;

public enum BookingStatus
{
    Active,
    Cancelled
}

public sealed class Booking
{
    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(4);

    private Booking() { } // for EF Core

    private Booking(Guid id, Guid roomId, string bookedBy, DateTime startUtc, DateTime endUtc, int attendees)
    {
        Id = id;
        RoomId = roomId;
        BookedBy = bookedBy;
        StartUtc = startUtc;
        EndUtc = endUtc;
        Attendees = attendees;
        Status = BookingStatus.Active;
    }

    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = null!;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    // nowUtc comes from TimeProvider in the handler; roomBookings are the existing bookings of this room.
    public static Result<Booking> Create(
        Room room,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc,
        IEnumerable<Booking> roomBookings)
    {
        if (string.IsNullOrWhiteSpace(bookedBy)) return BookingErrors.BookedByRequired;      // R2
        if (endUtc <= startUtc) return BookingErrors.InvalidRange;                           // R3
        if (attendees <= 0) return BookingErrors.AttendeesNotPositive;                       // R4
        if (attendees > room.Capacity) return BookingErrors.AttendeesExceedCapacity;         // R4
        if (startUtc < nowUtc) return BookingErrors.StartInPast;                             // R5
        if (endUtc - startUtc > MaxDuration) return BookingErrors.TooLong;                   // R6

        if (roomBookings.Any(b => b.Status == BookingStatus.Active && b.OverlapsWith(startUtc, endUtc)))
            return BookingErrors.RoomOccupied;                                               // R7

        return new Booking(Guid.NewGuid(), room.Id, bookedBy.Trim(), startUtc, endUtc, attendees);
    }

    // R7: strict comparison, so intervals that only touch do not overlap.
    public bool OverlapsWith(DateTime startUtc, DateTime endUtc) =>
        StartUtc < endUtc && startUtc < EndUtc;

    // R8: only an active booking can be cancelled.
    public Result Cancel()
    {
        if (Status == BookingStatus.Cancelled) return BookingErrors.AlreadyCancelled;

        Status = BookingStatus.Cancelled;
        return Result.Success();
    }
}
