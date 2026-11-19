namespace RoomHub.Api.Features.Bookings.CreateBooking;

// HTTP body; the room id comes from the route.
public sealed record CreateBookingRequest(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);
