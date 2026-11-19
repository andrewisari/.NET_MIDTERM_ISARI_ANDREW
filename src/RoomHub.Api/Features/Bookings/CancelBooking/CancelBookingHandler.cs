using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Models;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler(BookingRepository bookings) : ICommandHandler<CancelBookingCommand>
{
    public Task<Result> Handle(CancelBookingCommand command, CancellationToken ct)
    {
        var booking = bookings.Get(command.Id);
        if (booking is null)
            return Task.FromResult<Result>(BookingErrors.NotFound(command.Id));

        booking.Status = BookingStatus.Cancelled;
        return Task.FromResult(Result.Success());
    }
}
