using System.Collections.Generic;
using Xunit;
using Moq;
using TaskManagementApp;
public class CustomLoggerTests
{
    private readonly ConsoleLogger _logger;

    public CustomLoggerTests()
    {
        var loggerProvider = new ConsoleLoggerProvider();
        _logger = new ConsoleLogger("TestCategory", loggerProvider);
    }

    [Fact]
    public void LogInformation_ShouldLogMessage()
    {
        // Arrange
        var message = "Test info message";

        // Act
        _logger.LogInformation(message);

        // Assert
        var logs = _logger.GetLogs();  // Assuming your CustomLogger has GetLogs for testing
        Assert.Contains(message, logs);
    }

    [Fact]
    public void LogError_ShouldLogException()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act
        _logger.LogError(exception, "An error occurred");

        // Assert
        var logs = _logger.GetLogs();  // Replace with actual log retrieval logic
        Assert.Contains("An error occurred", logs);
        Assert.Contains("Test exception", logs);
    }
}
