using Microsoft.Extensions.Logging;
public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;


    public FileLoggerProvider(string filePath )
    {
        _filePath = filePath;
    }


    public ILogger CreateLogger(string categoryName)
    {
       return new FileLogger(_filePath, categoryName);
    }

    public void Dispose()
    {
        // Clean up if necessary
    }
}
