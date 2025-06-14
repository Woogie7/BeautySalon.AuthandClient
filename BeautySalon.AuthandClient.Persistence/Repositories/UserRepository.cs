using System;
using System.Threading.Tasks;
using BeautySalon.AuthandClient.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BeautySalon.AuthandClient.Domain.Entity;
using BeautySalon.AuthandClient.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AuthandClientDbContext _dbContext;
    public UserRepository(AuthandClientDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}