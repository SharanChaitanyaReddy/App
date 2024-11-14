using System;
using Microsoft.Extensions.Logging;
using Xunit;

public class ConsoleLoggerTests
{
    [Fact]
    public void IsEnabled_AlwaysReturnsTrue()
    {
        // Arrange
        var logger = new ConsoleLogger("TestCategory");

        // Act
        var isEnabled = logger.IsEnabled(LogLevel.Information);

        // Assert
        Assert.True(isEnabled);
    }

    [Fact]
    public void Log_WritesFormattedMessageToConsole()
    {
        // Arrange
        var logger = new ConsoleLogger("TestCategory");
        var logLevel = LogLevel.Information;
        var eventId = new EventId(1, "TestEvent");
        string expectedMessage = "Information: - This is a test log message.";

        // Capture console output
        using var consoleOutput = new ConsoleOutputCapture();
        
        // Act
        logger.Log(logLevel, eventId, "This is a test log message.", null, (state, ex) => state.ToString());

        // Assert
        Assert.Contains(expectedMessage, consoleOutput.GetOutput());
    }

    [Fact]
    public void Log_IncludesExceptionMessage_WhenExceptionIsNotNull()
    {
        // Arrange
        var logger = new ConsoleLogger("TestCategory");
        var logLevel = LogLevel.Error;
        var eventId = new EventId(1, "TestEvent");
        var exception = new InvalidOperationException("Test exception");
        string expectedMessage = "Error: - Log with exception Test exception";

        // Capture console output
        using var consoleOutput = new ConsoleOutputCapture();

        // Act
        logger.Log(logLevel, eventId, "Log with exception", exception, (state, ex) => $"{state} {ex?.Message}");

        // Assert
        Assert.Contains(expectedMessage, consoleOutput.GetOutput());
    }
}

// Helper class to capture console output
public class ConsoleOutputCapture : IDisposable
{
    private readonly System.IO.StringWriter _stringWriter;
    private readonly System.IO.TextWriter _originalOutput;

    public ConsoleOutputCapture()
    {
        _stringWriter = new System.IO.StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOutput()
    {
        return _stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}
