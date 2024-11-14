using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TaskManagementApp.Models;
using Xunit;

namespace TaskManagementApp.Tests.Controllers
{
    public class TaskApiControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly TaskApiController _controller;

        public TaskApiControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _controller = new TaskApiController(_taskServiceMock.Object);
        }

        [Fact]
        public async Task GetTask_ReturnsOkResult_WithTaskItem()
        {
            // Arrange
            var taskId = 1;
            var taskItem = new TaskItem { Id = taskId, Title = "Updated Task", Description="Description"  };
            _taskServiceMock.Setup(service => service.GetTaskByIdAsync(taskId))
                            .ReturnsAsync(taskItem);

            // Act
            var result = await _controller.GetTask(taskId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<TaskItem>(okResult.Value);
            Assert.Equal(taskId, returnedTask.Id);
        }

        [Fact]
        public async Task GetTask_ReturnsNotFound_WhenTaskIsNull()
        {
            // Arrange
            var taskId = 2;
            _taskServiceMock.Setup(service => service.GetTaskByIdAsync(taskId))
                            .ReturnsAsync((TaskItem)null);

            // Act
            var result = await _controller.GetTask(taskId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateTask_ReturnsCreatedAtAction_WithTaskItem()
        {
            // Arrange
            var newTask = new TaskItem { Id = 3, Title = "new Task", Description="Description"  };
            _taskServiceMock.Setup(service => service.CreateTaskAsync(newTask))
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateTask(newTask);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedTask = Assert.IsType<TaskItem>(createdAtActionResult.Value);
            Assert.Equal(newTask.Id, returnedTask.Id);
            Assert.Equal("GetTask", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task CreateTask_ThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var newTask = new TaskItem { Id = 4, Title = "Updated Task", Description="Description"  };
            _taskServiceMock.Setup(service => service.CreateTaskAsync(newTask))
                            .ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _controller.CreateTask(newTask);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Error creating task", badRequestResult.Value);
        }
    }
}
