using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp;
public class CommentControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly Mock<ICommentService> _commentServiceMock;
    private readonly CommentController _controller;

    public CommentControllerTests()
    {
        // Set up DI container
        var services = new ServiceCollection();

        // Mock dependencies
        _commentServiceMock = new Mock<ICommentService>();
        
        // Register mock services with the DI container
        services.AddSingleton<ICommentService>(_commentServiceMock.Object);
        

        _serviceProvider = services.BuildServiceProvider();

        // Resolve TaskController with dependencies from DI container
        _controller = new CommentController(_serviceProvider.GetService<ICommentService>());
    }

    [Fact]
    public async Task GetCommentsByTaskId_ShouldReturnCommentsForGivenTask()
    {
        // Arrange
        var taskId = 1;
        var comments = new List<CommentDto> { new CommentDto { Content = "Test Comment" } };
        _commentServiceMock.Setup(service => service.GetCommentsByTaskIdAsync(taskId)).ReturnsAsync(comments);

        // Act
        var result = await _controller.GetCommentsByTaskId(taskId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedComments = Assert.IsType<List<CommentDto>>(okResult.Value);
        Assert.Single(returnedComments);
    }

    [Fact]
    public async Task AddComment_ShouldReturnOk_WhenCommentIsAdded()
    {
        // Arrange
        var commentDto = new CommentDto { Content = "New Comment" };
        _commentServiceMock.Setup(service => service.AddCommentAsync(commentDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddComment(commentDto);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
