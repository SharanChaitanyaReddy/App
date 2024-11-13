using System.Collections.Generic;
using Xunit;
using Moq;
using TaskManagementApp;
using TaskManagementApp.Models;

namespace  TaskManagementApp.Tests.Repository
{
    public class CommentRepositoryTests
    {
        private readonly TaskDbContext _context;
        private readonly ICommentRepository _commentRepository;

        public CommentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationDbContext(options);
            _commentRepository = new CommentRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddComment()
        {
            // Arrange
            var comment = new Comment { Content = "Test Comment", UserId = 1, TaskId = 1 };

            // Act
            await _commentRepository.AddAsync(comment);
            var result = await _context.Comments.FindAsync(comment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comment.Content, result.Content);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllComments()
        {
            // Arrange
            _context.Comments.Add(new Comment { TaskId = 1, Content = "Test Comment 1" });
            _context.Comments.Add(new Comment { TaskId = 1, Content = "Test Comment 2" });
            await _context.SaveChangesAsync();

            // Act
            var comments = (IEnumerable<Comment>) _commentRepository.GetAllByTaskIdAsync(1);
            

            // Assert
            Assert.Equal(2, comments.Count());
        }
    }
}