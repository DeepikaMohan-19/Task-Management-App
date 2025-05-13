using Microsoft.EntityFrameworkCore;
using TMA.Domain.Interfaces.Repositories;
using TMA.Infrastructure.Persistence;

namespace TMA.Infrastructure.Repositories
{

    public class BaseRepository<TEntity>(IDbContextFactory<TaskDBContext> dbContextFactory) : IBaseRepository<TEntity> where TEntity : class
    {
        public async Task AddAsync(TEntity entity)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            dbContext.Set<TEntity>().Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Set<TEntity>().ToListAsync();
        }
    }
}
