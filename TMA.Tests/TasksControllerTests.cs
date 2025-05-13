using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TMA.Application.DTOs;
using TMA.Application.Services.Interfaces;
using TMA.Controllers;

public class TasksControllerTests
{
    private readonly Mock<ITasksService> _mockService;
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _mockService = new Mock<ITasksService>();
        _controller = new TasksController(_mockService.Object);

        // Mock user identity
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, "test@example.com")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task GetTaskById_ReturnsOk_WhenTaskExists()
    {
        var taskId = Guid.NewGuid();
        var task = new TaskDto { Id = taskId };
        _mockService.Setup(s => s.GetTaskByIdAsync(taskId)).ReturnsAsync(task);

        var result = await _controller.GetTaskById(taskId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(task, okResult.Value);
    }

    [Fact]
    public async Task GetTaskById_ReturnsNotFound_WhenTaskDoesNotExist()
    {
        var taskId = Guid.NewGuid();
        _mockService.Setup(s => s.GetTaskByIdAsync(taskId)).ReturnsAsync((TaskDto)null!);

        var result = await _controller.GetTaskById(taskId);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddTask_ReturnsCreated_WhenValid()
    {
        var task = new TaskDto { Id = Guid.NewGuid() };

        var result = await _controller.AddTask(task);

        _mockService.Verify(s => s.AddTaskAsync(It.Is<TaskDto>(t => t.CreatedByEmail == "test@example.com")), Times.Once);
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task AddTask_ReturnsBadRequest_WhenNull()
    {
        var result = await _controller.AddTask(null!);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsNoContent_WhenValid()
    {
        var taskId = Guid.NewGuid();
        var task = new TaskDto { Id = taskId };

        var result = await _controller.UpdateTask(taskId, task);

        _mockService.Verify(s => s.UpdateTaskAsync(task), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenInvalid()
    {
        var result = await _controller.UpdateTask(Guid.NewGuid(), null!);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateTask_ReturnsBadRequest_WhenIdMismatch()
    {
        var task = new TaskDto { Id = Guid.NewGuid() };
        var result = await _controller.UpdateTask(Guid.NewGuid(), task);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNoContent()
    {
        var taskId = Guid.NewGuid();
        var result = await _controller.DeleteTask(taskId);

        _mockService.Verify(s => s.DeleteTaskAsync(taskId), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetTasks_ReturnsTasksForUser()
    {
        var userEmail = "test@example.com";
        var tasks = new List<TaskDto> { new TaskDto { Id = Guid.NewGuid() } };

        _mockService.Setup(s => s.GetTasksByUserIdAsync(userEmail)).ReturnsAsync(tasks);

        var result = await _controller.GetTasks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(tasks, okResult.Value);
    }
}
