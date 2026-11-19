using RoomHub.Api.Models;

namespace RoomHub.Api.Features.Bookings;

public sealed record BookingResponse(
    Guid Id,
    Guid RoomId,
    string BookedBy,
    DateTime StartUtc,
    DateTime EndUtc,
    int Attendees,
    string Status)
{
    public static BookingResponse From(Booking booking) =>
        new(booking.Id, booking.RoomId, booking.BookedBy, booking.StartUtc, booking.EndUtc,
            booking.Attendees, booking.Status.ToString());
}
