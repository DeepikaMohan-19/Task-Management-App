using Microsoft.EntityFrameworkCore;
using TMA.Domain.Entities;
using TMA.Domain.Interfaces.Repositories;
using TMA.Infrastructure.Persistence;

namespace TMA.Infrastructure.Repositories
{
    public class UsersRepository(IDbContextFactory<TaskDBContext> dbContextFactory) : BaseRepository<UserEntity>(dbContextFactory), IUsersRepository
    {
        public async Task<UserEntity?> GetByEmail(string email)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetByEmailAndPassword(string email, string password)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
