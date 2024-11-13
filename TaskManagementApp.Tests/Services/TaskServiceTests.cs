using System.Collections.Generic;
using Xunit;
using Moq;
using TaskManagementApp;
using Microsoft.Extensions.DependencyInjection;

public class TaskServiceTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        // Set up DI container
        var services = new ServiceCollection();

        // Mock dependencies
        _taskRepositoryMock = new Mock<ITaskRepository>();
        
        // Register mock services with the DI container
        services.AddSingleton<ITaskRepository>(_taskRepositoryMock.Object);
        

        _serviceProvider = services.BuildServiceProvider();

        // Resolve TaskController with dependencies from DI container
        _taskService = new TaskService(_serviceProvider.GetService<ITaskRepository>());
    }

    [Fact]
    public void AddTask_CallsRepositoryAddTask()
    {
        // Arrange
        var task = new Task { Name = "Sample Task" };

        // Act
        _taskService.AddTask(task);

        // Assert
        _taskRepositoryMock.Verify(repo => repo.AddTask(task), Times.Once);
    }

    [Fact]
    public void LinkTasks_ReturnsTrue_WhenSuccessful()
    {
        // Arrange
        var parentTaskId = 1;
        var subTaskId = 2;
        _taskRepositoryMock.Setup(repo => repo.LinkTasks(parentTaskId, subTaskId)).Returns(true);

        // Act
        var result = _taskService.LinkTasks(parentTaskId, subTaskId);

        // Assert
        Assert.True(result);
    }

}
