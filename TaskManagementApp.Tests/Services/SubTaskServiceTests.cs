using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp;
using  TaskManagementApp.Models;

namespace TaskManagementApp.Tests.Services{

    public class SubTaskServiceTests
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<ISubTaskRepository> _subTaskRepositoryMock;
        private readonly SubTaskService _subTaskService;

        public SubTaskServiceTests()
        {
            _subTaskRepositoryMock = new Mock<ISubTaskRepository>();
            _subTaskService = new SubTaskService(_subTaskRepositoryMock.Object);

            // Set up DI container
            var services = new ServiceCollection();

            // Mock dependencies
            _subTaskRepositoryMock = new Mock<ISubTaskRepository>();
            
            // Register mock services with the DI container
            services.AddSingleton<ISubTaskRepository>(_subTaskRepositoryMock.Object);
            

            _serviceProvider = services.BuildServiceProvider();

            // Resolve TaskController with dependencies from DI container
            _subTaskService = new SubTaskService(_serviceProvider.GetService<ISubTaskRepository>());
        }

        [Fact]
        public async Task CreateSubTaskAsync_ShouldCreateSubTask()
        {
            // Arrange
            var subTask = new SubTask { Title = "New SubTask", TaskItemId = 1 };
            _subTaskRepositoryMock.Setup(repo => repo.AddAsync(subTask)).Returns(Task.CompletedTask);

            // Act
            await _subTaskService.CreateSubTaskAsync(subTask);

            // Assert
            _subTaskRepositoryMock.Verify(repo => repo.AddAsync(subTask), Times.Once);
        }

        [Fact]
        public async Task GetSubTasksByTaskIdAsync_ShouldReturnSubTasksForTask()
        {
            // Arrange
            var taskId = 1;
            var subTasks = new IEnumerable<SubTask> { new SubTask { Title = "SubTask 1", ParentTaskId = taskId, ParentTask = new TaskItem{ Title= "task Item", Description = "Hello"  } } };
            _subTaskRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new List<SubTask> { new SubTask { Title = "SubTask 1", ParentTaskId = taskId, ParentTask = new TaskItem{ Title= "task Item", Description = "Hello"  } } });

            // Act
            var result = await _subTaskService.GetSubTasksByTaskIdAsync(taskId);

            // Assert
            Assert.Equal(subTasks.Count(), result.Count());
            Assert.Equal("SubTask 1", result.First().Title);
        }
    }

}