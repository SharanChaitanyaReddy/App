using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementApp.Models;
using Xunit;

namespace TaskManagementApp.Tests.Services
{
    public class SubTaskServiceTests
    {
        private readonly Mock<ISubTaskRepository> _subTaskRepositoryMock;
        private readonly SubTaskService _subTaskService;

        public SubTaskServiceTests()
        {
            _subTaskRepositoryMock = new Mock<ISubTaskRepository>();
            _subTaskService = new SubTaskService(_subTaskRepositoryMock.Object);
        }

        [Fact]
        public async Task GetSubTaskByIdAsync_ReturnsSubTask_WhenSubTaskExists()
        {
            // Arrange
            var subTaskId = 1;
            var subTask = new SubTask { Id = subTaskId, Title = "Updated SubTask", ParentTaskId = 1, ParentTask = new TaskItem{Id=1, Title="Title", Description="Description"} };
            _subTaskRepositoryMock.Setup(repo => repo.GetByIdAsync(subTaskId))
                                  .ReturnsAsync(subTask);

            // Act
            var result = await _subTaskService.GetSubTaskByIdAsync(subTaskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(subTaskId, result.Id);
            Assert.Equal("Sample SubTask", result.Title);
        }

        [Fact]
        public async Task GetSubTaskByIdAsync_ReturnsNull_WhenSubTaskDoesNotExist()
        {
            // Arrange
            var subTaskId = 2;
            _subTaskRepositoryMock.Setup(repo => repo.GetByIdAsync(subTaskId))
                                  .ReturnsAsync((SubTask)null);

            // Act
            var result = await _subTaskService.GetSubTaskByIdAsync(subTaskId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetSubTasksByTaskIdAsync_ReturnsSubTasks_WhenSubTasksExist()
        {
            // Arrange
            var taskId = 1;
            var subTasks = new List<SubTask>
            {
                new SubTask { Id = 4, Title = "Updated SubTask", ParentTaskId = 1, ParentTask = new TaskItem{Id=1, Title="Title", Description="Description"} },
                new SubTask { Id = 4, Title = "Updated SubTask", ParentTaskId = 1, ParentTask = new TaskItem{Id=1, Title="Title", Description="Description"} }
            };
            _subTaskRepositoryMock.Setup(repo => repo.GetAllByTaskIdAsync(taskId))
                                  .ReturnsAsync(subTasks);

            // Act
            var result = await _subTaskService.GetSubTasksByTaskIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(taskId, t.ParentTaskId));
        }

        [Fact]
        public async Task GetSubTasksByTaskIdAsync_ReturnsEmptyList_WhenNoSubTasksExist()
        {
            // Arrange
            var taskId = 1;
            _subTaskRepositoryMock.Setup(repo => repo.GetAllByTaskIdAsync(taskId))
                                  .ReturnsAsync(new List<SubTask>());

            // Act
            var result = await _subTaskService.GetSubTasksByTaskIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateSubTaskAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var subTask = new SubTask { Id = 3, Title = "Updated SubTask", ParentTask = new TaskItem{Id=1, Title="Title", Description="Description"} };

            // Act
            await _subTaskService.CreateSubTaskAsync(subTask);

            // Assert
            _subTaskRepositoryMock.Verify(repo => repo.AddAsync(subTask), Times.Once);
        }

        [Fact]
        public async Task UpdateSubTaskAsync_CallsRepositoryUpdateAsync()
        {
            // Arrange
            var subTask = new SubTask { Id = 4, Title = "Updated SubTask", ParentTask = new TaskItem{Id=1, Title="Title", Description="Description"} };

            // Act
            await _subTaskService.UpdateSubTaskAsync(subTask);

            // Assert
            _subTaskRepositoryMock.Verify(repo => repo.UpdateAsync(subTask), Times.Once);
        }

        [Fact]
        public async Task DeleteSubTaskAsync_CallsRepositoryDeleteAsync()
        {
            // Arrange
            var subTaskId = 5;

            // Act
            await _subTaskService.DeleteSubTaskAsync(subTaskId);

            // Assert
            _subTaskRepositoryMock.Verify(repo => repo.DeleteAsync(subTaskId), Times.Once);
        }
    }
}
