using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigurePipeline(app);

app.Run();

// Method to configure services
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add Controllers with Views
    services.AddControllersWithViews();

    // Configure Authentication with JWT Bearer
    ConfigureAuthentication(services);

    // Configure Authorization
    services.AddAuthorization();

     // Register the proxy factory
    services.AddSingleton<ProxyFactory>();

    // Dependency Injection for Repositories
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<ITaskRepository, TaskRepository>();
    services.AddScoped<ISubTaskRepository, SubTaskRepository>();
    services.AddScoped<ICommentRepository, CommentRepository>();
    services.AddScoped<IUserTaskHistoryRepository, UserTaskHistoryRepository>();
    services.AddScoped<ITaskLinkRepository, TaskLinkRepository>();

    // Dependency Injection for Services
    services.AddScoped<ITaskService, TaskService>();
    services.AddScoped<ISubTaskService, SubTaskService>();
    services.AddScoped<ICommentService, CommentService>();
    services.AddScoped<IUserTaskHistoryService, UserTaskHistoryService>();
    services.AddScoped<ITaskLinkService, TaskLinkService>();

    // Configure Database Context (PostgreSQL)
    services.AddDbContext<TaskDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    // Add Memory Cache
    services.AddMemoryCache();

    // Add Swagger (API Documentation)
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });
    });

    // Add gRPC Services
    services.AddGrpc();

    // Configure Logging
    ConfigureLogging(services);
}

// Method to configure authentication
void ConfigureAuthentication(IServiceCollection services)
{
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = "ItsMe",
                ValidAudience = "MyAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myKey"))
            };
        });
}

// Method to configure logging
void ConfigureLogging(IServiceCollection services)
{
    // Add Console and File loggers as providers
      var loggingBuilder = new LoggingBuilder();
    
    // Add Console and File loggers as providers using your custom LoggerBuilder
    loggingBuilder.AddProvider(new ConsoleLoggerProvider());
    loggingBuilder.AddProvider(new FileLoggerProvider("logs/app.log"));

    // Register ILoggerFactory and LoggingBuilder to DI container
    builder.Services.AddSingleton<ILoggerFactory>(serviceProvider =>
    {
        return new CustomLoggerFactory(loggingBuilder);
    });

}

// Method to configure the HTTP request pipeline
void ConfigurePipeline(WebApplication app)
{
    // Exception Handling Middleware
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    // Add custom request tracking middleware
    app.UseMiddleware<RequestTrackingMiddleware>();

    // Development-specific Configuration
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1"));
    }

    // Common Configuration
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    // Default Route Mapping
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}
