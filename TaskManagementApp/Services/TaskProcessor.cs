using  System.Threading.Tasks;
using TaskManagementApp.Models;
public class TaskProcessor{
    private readonly ITaskService _taskService;

    public TaskProcessor(ITaskService taskService){
        _taskService = taskService;
    }

    public async System.Threading.Tasks.Task ProcessTaskAsync(){
        var tasks = _taskService.GetAllTasks();

       // Gather high priority tasks
        var highPriorityTasks  = tasks.Where(t => t.Priority == TaskPriority.High).ToList();

        
        await ProcessHighPriorityTasksAsync(highPriorityTasks);

        var otherTasks = tasks.Where(t => t.Priority !=  TaskPriority.High).ToList();

        await ProcessHighPriorityTasksAsync(highPriorityTasks);

    }

    private System.Threading.Tasks.Task ProcessHighPriorityTasksAsync(List<TaskItem> tasks){

        return System.Threading.Tasks.Task.CompletedTask;
    }
}