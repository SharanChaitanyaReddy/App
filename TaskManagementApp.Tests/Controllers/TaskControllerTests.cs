using System.Collections.Generic;
using Xunit; // For writing unit tests
using Moq; // For creating mock objects
using Microsoft.AspNetCore.Mvc; // For controller-related types like OkObjectResult, CreatedAtActionResult, etc.
using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp; 

namespace TaskManagementApp.Tests.Controllers;

public class TaskControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly Mock<ITaskService> _taskServiceMock;
    private readonly TaskApiController _taskController;

    public TaskControllerTests()
    {

        // Set up DI container
        var services = new ServiceCollection();

        // Mock dependencies
        _taskServiceMock = new Mock<ITaskService>();
        
        // Register mock services with the DI container
        services.AddSingleton<ITaskService>(_taskServiceMock.Object);
        

        _serviceProvider = services.BuildServiceProvider();

        // Resolve TaskController with dependencies from DI container
        _taskController = new TaskController(_serviceProvider.GetService<ITaskService>());

    }

    [Fact]
    public void GetAllTasks_ReturnsOkResult()
    {
        // Arrange
        _taskServiceMock.Setup(service => service.GetAllTasks()).Returns(new List<Task>());

        // Act
        var result = _taskController.GetAllTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void CreateTask_ReturnsCreatedResult()
    {
        // Arrange
        var task = new Task { Id = 1, Name = "New Task" };
        _taskServiceMock.Setup(service => service.AddTask(It.IsAny<Task>())).Returns(task);

        // Act
        var result = _taskController.CreateTask(task);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(task, createdResult.Value);
    }

    [Fact]
    public void DeleteTask_ReturnsNoContentResult()
    {
        // Arrange
        var taskId = 1;
        _taskServiceMock.Setup(service => service.DeleteTask(taskId)).Returns(true);

        // Act
        var result = _taskController.DeleteTask(taskId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
