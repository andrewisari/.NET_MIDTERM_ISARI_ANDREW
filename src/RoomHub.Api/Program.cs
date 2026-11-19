using RoomHub.Api.Endpoints;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Middleware;
using RoomHub.Api.Repositories;
using RoomHub.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Starter organized in layers: Endpoints -> Services -> Repositories -> Models.
builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddRoomsFeature();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRoomsEndpoints();
app.MapRoomEndpoints();
app.MapBookingEndpoints();

app.Run();
