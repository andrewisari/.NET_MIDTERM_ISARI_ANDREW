using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(IBookingRepository bookings) : IQueryHandler<GetBookingQuery, BookingResponse>
{
    public async Task<Result<BookingResponse>> Handle(GetBookingQuery query, CancellationToken ct)
    {
        var booking = await bookings.GetByIdAsync(query.Id, ct);
        if (booking is null) return BookingErrors.NotFound(query.Id);

        return BookingResponse.From(booking);
    }
}
