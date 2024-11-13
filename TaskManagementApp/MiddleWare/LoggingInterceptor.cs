using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Reflection;

public class LoggingInterceptor : IInterceptor
{
    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggingInterceptor(ILogger logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Intercept(IInvocation invocation)
    {
        // Get the method and class name
        var className = invocation.TargetType.FullName;
        var methodName = invocation.Method.Name;

        // Retrieve the Tracking GUID from the HttpContext
        var trackingGuid = _httpContextAccessor.HttpContext?.Items["TrackingGuid"]?.ToString();

        try
        {
            // Log method entry with parameters (if any) and tracking GUID
            _logger.Log(LogLevel.Information,$"[{trackingGuid}] Entering {className}.{methodName} with parameters: {string.Join(", ", invocation.Arguments)}");

            // Proceed with the method execution
            invocation.Proceed();

            // Log method exit with tracking GUID
            _logger.Log(LogLevel.Information, $"[{trackingGuid}] Exiting {className}.{methodName}");
        }
        catch (Exception ex)
        {
            // Log exception with tracking GUID
            _logger.Log(LogLevel.Error, $"[{trackingGuid}] An error occurred in {className}.{methodName} Exception : {ex}");
            throw;  // Re-throw the exception after logging
        }
    }
}
