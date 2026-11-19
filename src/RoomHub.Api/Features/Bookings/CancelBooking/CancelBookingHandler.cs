using RoomHub.Api.Common;
using RoomHub.Api.Domain.Abstractions;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler(IBookingRepository bookings, IUnitOfWork unitOfWork)
    : ICommandHandler<CancelBookingCommand>
{
    public async Task<Result> Handle(CancelBookingCommand command, CancellationToken ct)
    {
        var booking = await bookings.GetByIdAsync(command.Id, ct);
        if (booking is null) return BookingErrors.NotFound(command.Id);

        var cancelled = booking.Cancel();
        if (cancelled.IsFailure) return cancelled;

        await unitOfWork.SaveChangesAsync(ct);
        return Result.Success();
    }
}
