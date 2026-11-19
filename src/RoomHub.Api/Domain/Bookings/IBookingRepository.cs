namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Booking>> ListForRoomAsync(Guid roomId, CancellationToken ct);
    Task<IReadOnlyList<Booking>> ListForParticipantOnDayAsync(string bookedBy, DateTime dayUtc, CancellationToken ct);
    void Add(Booking booking);
}
