using RoomHub.Api.Common;
using RoomHub.Api.Features.Bookings.CancelBooking;
using RoomHub.Api.Features.Bookings.CreateBooking;
using RoomHub.Api.Features.Bookings.GetBooking;

namespace RoomHub.Api.Features.Bookings;

public static class BookingsFeature
{
    public static IServiceCollection AddBookingsFeature(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateBookingCommand, BookingCreatedResponse>, CreateBookingHandler>();
        services.AddScoped<IQueryHandler<GetBookingQuery, BookingResponse>, GetBookingHandler>();
        services.AddScoped<ICommandHandler<CancelBookingCommand>, CancelBookingHandler>();
        return services;
    }

    public static IEndpointRouteBuilder MapBookingsEndpoints(this IEndpointRouteBuilder app)
    {
        CreateBookingEndpoint.Map(app);
        GetBookingEndpoint.Map(app);
        CancelBookingEndpoint.Map(app);
        return app;
    }
}
