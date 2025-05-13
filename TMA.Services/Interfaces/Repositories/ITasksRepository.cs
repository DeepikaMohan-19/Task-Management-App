using TMA.Domain.Entities;

namespace TMA.Domain.Interfaces.Repositories
{
    public interface ITasksRepository : IBaseRepository<TaskEntity>
    {
        Task<List<TaskEntity>> GetTasksByEmail(string email);
    }
}
