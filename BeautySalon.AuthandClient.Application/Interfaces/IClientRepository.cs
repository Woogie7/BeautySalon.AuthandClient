using BeautySalon.AuthandClient.Domain.Entity;

namespace BeautySalon.AuthandClient.Application.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client?> GetByUserIdAsync(Guid userId);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
}