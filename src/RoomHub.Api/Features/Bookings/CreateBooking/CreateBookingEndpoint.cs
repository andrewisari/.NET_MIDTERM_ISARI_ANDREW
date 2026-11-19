using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public static class CreateBookingEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("/api/rooms/{roomId:guid}/bookings", async (
            Guid roomId,
            CreateBookingRequest request,
            ICommandHandler<CreateBookingCommand, BookingCreatedResponse> handler,
            CancellationToken ct) =>
        {
            var command = new CreateBookingCommand(
                roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees);

            var result = await handler.Handle(command, ct);

            return result.IsSuccess
                ? Results.Created($"/api/bookings/{result.Value.Id}", result.Value)
                : result.ToProblem();
        });
}
