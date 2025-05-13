using AutoMapper;
using TMA.Application.DTOs;
using TMA.Application.Services.Interfaces;
using TMA.Domain.Entities;
using TMA.Domain.Interfaces.Repositories;

namespace TMA.Application.Services.Implementations
{
    public class TasksService(ITasksRepository tasksRepository, IMapper mapper) : ITasksService
    {
        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync() =>
            (await tasksRepository.GetAllAsync()).Select(x => mapper.Map<TaskDto>(x));

        public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
        {
            return mapper.Map<TaskDto>(await tasksRepository.GetByIdAsync(id));
        }
        public async Task AddTaskAsync(TaskDto task)
        {
            await tasksRepository.AddAsync(mapper.Map<TaskEntity>(task));
        }
        public async Task UpdateTaskAsync(TaskDto task)
        {
            await tasksRepository.UpdateAsync(mapper.Map<TaskEntity>(task));
        }
        public async Task DeleteTaskAsync(Guid id)
        {
            await tasksRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string email)
        {
            return await tasksRepository.GetTasksByEmail(email)
                .ContinueWith(t => t.Result.Select(x => mapper.Map<TaskDto>(x)));
        }
    }
}
