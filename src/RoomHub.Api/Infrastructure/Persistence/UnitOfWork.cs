using RoomHub.Api.Domain.Abstractions;

namespace RoomHub.Api.Infrastructure.Persistence;

// Same scoped AppDbContext as the repositories: one SaveChanges writes everything the handler changed.
public sealed class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}
