using RoomHub.Api.Common;
using RoomHub.Api.Domain.Abstractions;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, BookingCreatedResponse>
{
    public async Task<Result<BookingCreatedResponse>> Handle(CreateBookingCommand command, CancellationToken ct)
    {
        // The room must exist before any validation (404 first).
        var room = await rooms.GetByIdAsync(command.RoomId, ct);
        if (room is null) return RoomErrors.NotFound(command.RoomId);

        var roomBookings = await bookings.ListForRoomAsync(command.RoomId, ct);

        // R9 needs the participant's other bookings that day; the domain decides with them.
        IReadOnlyList<Booking> participantBookings = string.IsNullOrWhiteSpace(command.BookedBy)
            ? []
            : await bookings.ListForParticipantOnDayAsync(command.BookedBy.Trim(), command.StartUtc, ct);

        var created = Booking.Create(
            room,
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            timeProvider.GetUtcNow().UtcDateTime,
            roomBookings,
            participantBookings);
        if (created.IsFailure) return created.Error;

        var booking = created.Value;
        bookings.Add(booking);
        await unitOfWork.SaveChangesAsync(ct);

        return new BookingCreatedResponse(booking.Id, booking.RoomId, booking.Status.ToString());
    }
}
