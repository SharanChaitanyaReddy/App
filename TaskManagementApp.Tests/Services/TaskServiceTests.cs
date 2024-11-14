using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementApp.Models;
using Xunit;

namespace TaskManagementApp.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<TaskRepository> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<TaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsTask_WhenTaskExists()
        {
            // Arrange
            var taskId = 1;
            var taskItem = new TaskItem { Id = taskId, Title = "Sample Task" , Description="Description"};
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId))
                               .ReturnsAsync(taskItem);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result.Id);
            Assert.Equal("Sample Task", result.Title);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ReturnsNull_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = 2;
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId))
                               .ReturnsAsync((TaskItem)null);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllTasksAsync_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1" , Description="Description"},
                new TaskItem { Id = 2, Title = "Task 2" , Description="Description"}
            };
            _taskRepositoryMock.Setup(repo => repo.GetAllAsync())
                               .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, t => t.Title == "Task 1");
            Assert.Contains(result, t => t.Title == "Task 2");
        }

        [Fact]
        public async Task CreateTaskAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var taskItem = new TaskItem { Id = 3, Title = "Updated Task", Description="Description"  };

            // Act
            await _taskService.CreateTaskAsync(taskItem);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.AddAsync(taskItem), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var taskItem = new TaskItem { Id = 4, Title = "Updated Task", Description="Description" };

            // Act
            await _taskService.UpdateTaskAsync(taskItem);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateAsync(taskItem), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_CallsRepositoryDeleteAsync()
        {
            // Arrange
            var taskId = 5;

            // Act
            await _taskService.DeleteTaskAsync(taskId);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
        }
    }
}
