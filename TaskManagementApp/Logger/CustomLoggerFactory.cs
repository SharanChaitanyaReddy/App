public class CustomLoggerFactory : ILoggerFactory
{
    private readonly LoggingBuilder _loggingBuilder;

    public CustomLoggerFactory(LoggingBuilder loggingBuilder)
    {
        _loggingBuilder = loggingBuilder;
    }

    public ILogger CreateLogger(string categoryName)
    {
        // Select the appropriate logger provider based on category or configuration
        // Here, we simply pick the first provider for simplicity
        var provider = _loggingBuilder.GetProviders().First();
        return provider.CreateLogger(categoryName);
    }

    public void Dispose()
    {
        // Dispose of any resources if necessary
    }

    public void AddProvider(ILoggerProvider provider)
    {
        _loggingBuilder.AddProvider(provider);
    }
}
