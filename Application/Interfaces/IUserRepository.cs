using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
}