using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class RequestTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate a unique GUID for the request
        var trackingGuid = Guid.NewGuid().ToString();

        // Store the GUID in the request's items collection
        context.Items["TrackingGuid"] = trackingGuid;

        // Continue with the request processing
        await _next(context);
    }
}
