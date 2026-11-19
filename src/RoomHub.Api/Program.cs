using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Framework errors (malformed JSON, missing body) are also returned as ProblemDetails.
builder.Services.AddProblemDetails();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddRoomsFeature();
builder.Services.AddBookingsFeature();

var app = builder.Build();

// Error responses without a body (for example 400 from binding outside Development) get a ProblemDetails body.
app.UseStatusCodePages();

app.Services.CreateDatabase();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRoomsEndpoints();
app.MapBookingsEndpoints();

app.Run();
