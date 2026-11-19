using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(BookingRepository bookings) : IQueryHandler<GetBookingQuery, BookingResponse>
{
    public Task<Result<BookingResponse>> Handle(GetBookingQuery query, CancellationToken ct)
    {
        var booking = bookings.Get(query.Id);
        if (booking is null)
            return Task.FromResult<Result<BookingResponse>>(BookingErrors.NotFound(query.Id));

        return Task.FromResult<Result<BookingResponse>>(BookingResponse.From(booking));
    }
}
