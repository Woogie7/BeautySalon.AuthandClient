using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Domain.Entity;
using BeautySalon.AuthandClient.Persistence;
using Microsoft.EntityFrameworkCore;

public class ClientRepository : IClientRepository
{
    private readonly AuthandClientDbContext _dbContext;

    public ClientRepository(AuthandClientDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Clients.FindAsync(id);
    }

    public async Task<Client?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task AddAsync(Client client)
    {
        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _dbContext.Clients.Update(client);
        await _dbContext.SaveChangesAsync();
    }
}