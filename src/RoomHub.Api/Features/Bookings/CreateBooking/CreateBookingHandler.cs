using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Models;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(RoomRepository rooms, BookingRepository bookings)
    : ICommandHandler<CreateBookingCommand, BookingCreatedResponse>
{
    public Task<Result<BookingCreatedResponse>> Handle(CreateBookingCommand command, CancellationToken ct) =>
        Task.FromResult(Create(command));

    private Result<BookingCreatedResponse> Create(CreateBookingCommand command)
    {
        var room = rooms.Get(command.RoomId);
        if (room is null) return RoomErrors.NotFound(command.RoomId);

        if (string.IsNullOrWhiteSpace(command.BookedBy)) return BookingErrors.BookedByRequired;
        if (command.EndUtc <= command.StartUtc) return BookingErrors.InvalidRange;
        if (command.Attendees <= 0) return BookingErrors.AttendeesNotPositive;
        if (command.Attendees > room.Capacity) return BookingErrors.AttendeesExceedCapacity;

        foreach (var existing in bookings.ListForRoom(command.RoomId))
        {
            if (existing.StartUtc <= command.EndUtc && command.StartUtc <= existing.EndUtc)
                return BookingErrors.RoomOccupied;
        }

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = command.RoomId,
            BookedBy = command.BookedBy.Trim(),
            StartUtc = command.StartUtc,
            EndUtc = command.EndUtc,
            Attendees = command.Attendees,
            Status = BookingStatus.Active
        };

        bookings.Add(booking);
        return new BookingCreatedResponse(booking.Id, booking.RoomId, booking.Status.ToString());
    }
}
