using BeautySalon.AuthandClient.Domain.Entity;

namespace BeautySalon.AuthandClient.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}