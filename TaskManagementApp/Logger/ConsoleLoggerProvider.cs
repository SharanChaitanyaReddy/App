public class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string category)
    {
        return new ConsoleLogger(category);
    }

    public void Dispose()
    {
        // Clean up if necessary
    }
}
