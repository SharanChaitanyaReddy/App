using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementApp.Models;
using Xunit;

namespace TaskManagementApp.Tests.Repositories
{
    public class CommentRepositoryTests
    {
        private readonly CommentRepository _commentRepository;
        private readonly TaskDbContext _context;

        public CommentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new TaskDbContext(options);
            _commentRepository = new CommentRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsComment_WhenCommentExists()
        {
            // Arrange
            var comment = new Comment { Id = 1, TaskId = 1, Content = "Test Comment" };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _commentRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Comment", result.Content);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenCommentDoesNotExist()
        {
            // Act
            var result = await _commentRepository.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllByTaskIdAsync_ReturnsComments_WhenCommentsExistForTask()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1, TaskId = 1, Content = "Comment 1" },
                new Comment { Id = 2, TaskId = 1, Content = "Comment 2" },
                new Comment { Id = 3, TaskId = 2, Content = "Comment 3" }
            };
            _context.Comments.AddRange(comments);
            await _context.SaveChangesAsync();

            // Act
            var result = await _commentRepository.GetAllByTaskIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, c => Assert.Equal(1, c.TaskId));
        }

        [Fact]
        public async Task GetAllByTaskIdAsync_ReturnsEmptyList_WhenNoCommentsExistForTask()
        {
            // Act
            var result = await _commentRepository.GetAllByTaskIdAsync(99);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddAsync_AddsCommentToDatabase()
        {
            // Arrange
            var comment = new Comment { Id = 4, TaskId = 1, Content = "New Comment" };

            // Act
            await _commentRepository.AddAsync(comment);
            var result = await _context.Comments.FindAsync(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Comment", result.Content);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingComment()
        {
            // Arrange
            var comment = new Comment { Id = 5, TaskId = 1, Content = "Original Content" };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Act
            comment.Content = "Updated Content";
            await _commentRepository.UpdateAsync(comment);
            var updatedComment = await _context.Comments.FindAsync(5);

            // Assert
            Assert.NotNull(updatedComment);
            Assert.Equal("Updated Content", updatedComment.Content);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCommentFromDatabase_WhenCommentExists()
        {
            // Arrange
            var comment = new Comment { Id = 6, TaskId = 1, Content = "Comment to Delete" };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Act
            await _commentRepository.DeleteAsync(6);
            var deletedComment = await _context.Comments.FindAsync(6);

            // Assert
            Assert.Null(deletedComment);
        }

        [Fact]
        public async Task DeleteAsync_DoesNothing_WhenCommentDoesNotExist()
        {
            // Act
            await _commentRepository.DeleteAsync(99);

            // Assert
            Assert.Equal(0, await _context.Comments.CountAsync());
        }
    }
}
