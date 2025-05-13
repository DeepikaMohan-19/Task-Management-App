using TMA.Application.DTOs;

namespace TMA.Application.Services.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto?> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(TaskDto task);
        Task UpdateTaskAsync(TaskDto task);
        Task DeleteTaskAsync(Guid id);
        Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string email);
    }
}
