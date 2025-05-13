using Microsoft.EntityFrameworkCore;
using TMA.Domain.Entities;
using TMA.Domain.Interfaces.Repositories;
using TMA.Infrastructure.Persistence;

namespace TMA.Infrastructure.Repositories
{
    public class TasksRepository(IDbContextFactory<TaskDBContext> dbContextFactory) : BaseRepository<TaskEntity>(dbContextFactory), ITasksRepository
    {
        public async Task<List<TaskEntity>> GetTasksByEmail(string email)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Tasks.Where(t => t.CreatedByEmail == email)
                .ToListAsync();
        }
    }
}
