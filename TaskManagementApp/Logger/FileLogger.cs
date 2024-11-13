using System;
using System.IO;
using Microsoft.Extensions.Logging;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    
    private readonly string _categoryName;

    public FileLogger(string filePath, string categoryName)
    {
        _filePath = filePath;
        _categoryName = categoryName;
    }


    public void Log(LogLevel logLevel, string message)
    {
        var logMessage = $"{DateTime.Now} [{logLevel}] {message}";
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
    }

    public void Log(LogLevel logLevel, string message, Exception exception)
    {
        var logMessage = $"{DateTime.Now} [{logLevel}] {message}. Exception: {exception.Message}";
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
    }

     public IDisposable BeginScope<TState>(TState state)
    {
        // Optionally implement scope behavior
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // Decide whether to log based on the log level
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        // Custom logging logic (e.g., log to console)
        var logMessage = $"{DateTime.Now} [{logLevel}]. Exception: {exception.Message}";
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
    }
}
