using TaskManagementApp.Models;

public class TaskExpiryService
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskExpiryService> _logger;

    public TaskExpiryService(ITaskService taskService, ILogger<TaskExpiryService> logger){
        _taskService = taskService;
        _logger = logger;
    }

    public void StartCheckingExpiry(){
        var timer = new Timer(CheckForExpiredTasks, null, 
        TimeSpan.Zero, TimeSpan.FromMinutes(5));

    }

    private void CheckForExpiredTasks(object? state)
    {
        var Tasks = (IEnumerable<TaskItem>)_taskService.GetAllTasksAsync();
        var expiredTasks = Tasks.Where(t => t.ExpiryDate < DateTime.Now).ToList();

        foreach(var task in expiredTasks){
            task.Status = TaskStatus.Done;
            _taskService.UpdateTaskAsync(task);
        }
    }

}