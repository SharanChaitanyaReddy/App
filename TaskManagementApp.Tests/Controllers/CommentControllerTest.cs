using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementApp.Models;
using Xunit;

namespace TaskManagementApp.Tests.Controllers
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly CommentController _controller;

        public CommentControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _controller = new CommentController(_commentServiceMock.Object);
        }

        [Fact]
        public async Task GetCommentById_ReturnsOkResult_WithComment()
        {
            // Arrange
            var commentId = 1;
            var comment = new Comment { Id = commentId, Content = "Sample comment" };
            _commentServiceMock.Setup(service => service.GetCommentByIdAsync(commentId))
                               .ReturnsAsync(comment);

            // Act
            var result = await _controller.GetCommentById(commentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedComment = Assert.IsType<Comment>(okResult.Value);
            Assert.Equal(commentId, returnedComment.Id);
        }

        [Fact]
        public async Task GetCommentById_ReturnsNotFound_WhenCommentIsNull()
        {
            // Arrange
            var commentId = 2;
            _commentServiceMock.Setup(service => service.GetCommentByIdAsync(commentId))
                               .ReturnsAsync((Comment)null);

            // Act
            var result = await _controller.GetCommentById(commentId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCommentsByTaskId_ReturnsOkResult_WithComments()
        {
            // Arrange
            var taskId = 1;
            var comments = new List<Comment>
            {
                new Comment { Id = 1, TaskId = taskId, Content = "Comment 1" },
                new Comment { Id = 2, TaskId = taskId, Content = "Comment 2" }
            };
            _commentServiceMock.Setup(service => service.GetCommentsByTaskIdAsync(taskId))
                               .ReturnsAsync(comments);

            // Act
            var result = await _controller.GetCommentsByTaskId(taskId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedComments = Assert.IsAssignableFrom<IEnumerable<Comment>>(okResult.Value);
            Assert.Equal(2, returnedComments.Count());
        }

        [Fact]
        public async Task GetCommentsByTaskId_ReturnsNotFound_WhenNoCommentsExist()
        {
            // Arrange
            var taskId = 2;
            _commentServiceMock.Setup(service => service.GetCommentsByTaskIdAsync(taskId))
                               .ReturnsAsync(new List<Comment>());

            // Act
            var result = await _controller.GetCommentsByTaskId(taskId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateComment_ReturnsCreatedAtAction_WithComment()
        {
            // Arrange
            var comment = new Comment { Id = 3, Content = "New Comment" };
            _commentServiceMock.Setup(service => service.CreateCommentAsync(comment))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateComment(comment);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedComment = Assert.IsType<Comment>(createdAtActionResult.Value);
            Assert.Equal(comment.Id, returnedComment.Id);
            Assert.Equal("GetCommentById", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task CreateComment_ReturnsBadRequest_WhenCommentIsNull()
        {
            // Act
            var result = await _controller.CreateComment(null);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateComment_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var commentId = 4;
            var comment = new Comment { Id = commentId, Content = "Updated Comment" };
            _commentServiceMock.Setup(service => service.UpdateCommentAsync(comment))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateComment(commentId, comment);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateComment_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var commentId = 4;
            var comment = new Comment { Id = 5, Content="Content"}; // Different ID

            // Act
            var result = await _controller.UpdateComment(commentId, comment);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteComment_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var commentId = 6;
            _commentServiceMock.Setup(service => service.DeleteCommentAsync(commentId))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteComment(commentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
