using RoomHub.Api.Common;
using RoomHub.Api.Features.Rooms.CreateRoom;
using RoomHub.Api.Features.Rooms.GetRoom;

namespace RoomHub.Api.Features.Rooms;

public static class RoomsFeature
{
    public static IServiceCollection AddRoomsFeature(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateRoomCommand, RoomResponse>, CreateRoomHandler>();
        services.AddScoped<IQueryHandler<GetRoomQuery, RoomResponse>, GetRoomHandler>();
        return services;
    }

    public static IEndpointRouteBuilder MapRoomsEndpoints(this IEndpointRouteBuilder app)
    {
        CreateRoomEndpoint.Map(app);
        GetRoomEndpoint.Map(app);
        return app;
    }
}
