using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// In-memory stores until the persistence stage.
builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<BookingRepository>();

builder.Services.AddRoomsFeature();
builder.Services.AddBookingsFeature();

var app = builder.Build();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRoomsEndpoints();
app.MapBookingsEndpoints();

app.Run();
