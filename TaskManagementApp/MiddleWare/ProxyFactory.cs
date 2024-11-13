using Castle.DynamicProxy;

public class ProxyFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ProxyGenerator _proxyGenerator;

    public ProxyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _proxyGenerator = new ProxyGenerator();
    }

    public T CreateWithLogging<T>() where T : class
    {
        var service = _serviceProvider.GetService<T>();
        var logger = _serviceProvider.GetService<ILogger<T>>(); // Get the logger (custom one)
        var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>(); // Get HttpContextAccessor

        // Create the proxy with logging capabilities
        return _proxyGenerator.CreateInterfaceProxyWithTarget(service, new LoggingInterceptor(logger, httpContextAccessor));
    }
}
