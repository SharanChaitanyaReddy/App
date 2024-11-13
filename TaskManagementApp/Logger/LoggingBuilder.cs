public class LoggingBuilder : ILoggingBuilder
{
    private readonly List<ILoggerProvider> _providers = new List<ILoggerProvider>();

    public void AddProvider(ILoggerProvider provider)
    {
        _providers.Add(provider);
    }

    public IEnumerable<ILoggerProvider> GetProviders()
    {
        return _providers;
    }
}
