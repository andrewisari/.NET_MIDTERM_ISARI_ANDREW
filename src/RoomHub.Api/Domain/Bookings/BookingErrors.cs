using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Booking.NotFound", $"The booking '{id}' does not exist.");

    public static readonly Error BookedByRequired =
        Error.Validation("Booking.BookedByRequired", "BookedBy is required.");

    public static readonly Error InvalidRange =
        Error.Validation("Booking.InvalidRange", "The end must be after the start.");

    public static readonly Error AttendeesNotPositive =
        Error.Validation("Booking.AttendeesNotPositive", "Attendees must be positive.");

    public static readonly Error AttendeesExceedCapacity =
        Error.Validation("Booking.AttendeesExceedCapacity", "The room does not have enough seats.");

    public static readonly Error StartInPast =
        Error.Validation("Booking.StartInPast", "A booking cannot start in the past.");

    public static Error TooLong(TimeSpan max) =>
        Error.Validation("Booking.TooLong", $"A booking in this room can last at most {max.TotalHours:0.##} hours.");

    public static readonly Error RoomOccupied =
        Error.Conflict("Booking.RoomOccupied", "The room is occupied in this interval.");

    public static readonly Error DailyLimitReached =
        Error.Conflict("Booking.DailyLimitReached", "A participant can have at most 2 active bookings per day.");

    public static readonly Error CancellationTooLate =
        Error.Conflict("Booking.CancellationTooLate", "A booking can be cancelled only at least 60 minutes before it starts.");

    public static readonly Error AlreadyCancelled =
        Error.Conflict("Booking.AlreadyCancelled", "The booking is already cancelled.");
}
