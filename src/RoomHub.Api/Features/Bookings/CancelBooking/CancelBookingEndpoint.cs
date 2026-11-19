using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class CancelBookingEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("/api/bookings/{id:guid}/cancel", async (
            Guid id,
            ICommandHandler<CancelBookingCommand> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(new CancelBookingCommand(id), ct);

            return result.IsSuccess ? Results.NoContent() : result.ToProblem();
        });
}
