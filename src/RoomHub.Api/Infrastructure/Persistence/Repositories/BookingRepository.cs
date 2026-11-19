using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository(AppDbContext db) : IBookingRepository
{
    // Tracked on purpose: CancelBooking changes the entity and the Unit of Work saves it.
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct) =>
        db.Bookings.FirstOrDefaultAsync(b => b.Id == id, ct);

    public async Task<IReadOnlyList<Booking>> ListForRoomAsync(Guid roomId, CancellationToken ct) =>
        await db.Bookings.AsNoTracking().Where(b => b.RoomId == roomId).ToListAsync(ct);

    public void Add(Booking booking) => db.Bookings.Add(booking);
}
