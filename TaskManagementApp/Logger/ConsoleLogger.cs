using System;
using Microsoft.Extensions.Logging;

public class ConsoleLogger : ILogger
{
    private readonly string _categoryName;

    public ConsoleLogger(string categoryName)
    {
        _categoryName = categoryName;
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
        Console.WriteLine($"{logLevel}: - {formatter(state, exception)}");
    }
}
