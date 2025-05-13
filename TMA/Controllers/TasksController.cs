using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMA.Application.DTOs;
using TMA.Application.Services.Interfaces;

namespace TMA.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "UserOrAdmin")]
    [ApiController]
    public class TasksController(ITasksService tasksService) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await tasksService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDto task)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (task == null)
            {
                return BadRequest();
            }

            task.CreatedByEmail = userEmail;
            await tasksService.AddTaskAsync(task);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDto task)
        {
            if (task == null || id != task.Id)
            {
                return BadRequest();
            }
            await tasksService.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await tasksService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var tasks = await tasksService.GetTasksByUserIdAsync(userEmail!);
            return Ok(tasks);
        }
    }
}
