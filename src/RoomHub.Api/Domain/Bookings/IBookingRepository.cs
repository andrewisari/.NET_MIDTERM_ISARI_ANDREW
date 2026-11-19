namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Booking>> ListForRoomAsync(Guid roomId, CancellationToken ct);
    void Add(Booking booking);
}
