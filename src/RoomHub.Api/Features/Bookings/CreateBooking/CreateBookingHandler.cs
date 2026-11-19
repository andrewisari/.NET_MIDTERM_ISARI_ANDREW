using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(RoomRepository rooms, BookingRepository bookings, TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, BookingCreatedResponse>
{
    public Task<Result<BookingCreatedResponse>> Handle(CreateBookingCommand command, CancellationToken ct) =>
        Task.FromResult(Create(command));

    private Result<BookingCreatedResponse> Create(CreateBookingCommand command)
    {
        // The room must exist before any validation (404 first).
        var room = rooms.Get(command.RoomId);
        if (room is null) return RoomErrors.NotFound(command.RoomId);

        var created = Booking.Create(
            room,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            timeProvider.GetUtcNow().UtcDateTime,
            bookings.ListForRoom(command.RoomId));
        if (created.IsFailure) return created.Error;

        var booking = created.Value;
        bookings.Add(booking);
        return new BookingCreatedResponse(booking.Id, booking.RoomId, booking.Status.ToString());
    }
}
