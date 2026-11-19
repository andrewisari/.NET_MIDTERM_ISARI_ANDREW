namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed record BookingCreatedResponse(Guid Id, Guid RoomId, string Status);
