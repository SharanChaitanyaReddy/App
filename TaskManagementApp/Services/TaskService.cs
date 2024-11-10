using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Models;
public class TaskService
{
 private readonly TaskRepository _taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItem> CreateTaskAsync(string title, string description, TaskPriority priority, DateTime dueDate)
    {
        var task = new TaskItem
        {
            Title = title,
            Description = description,
            Priority = priority,
            Status = TaskStatus.ToDo,
            DueDate = dueDate,
            SubTasks = new List<SubTask>(),
            Comments = new List<Comment>()
        };
        
        return await _taskRepository.CreateTaskAsync(task);
    }
    
    public async System.Threading.Tasks.Task AddCommentAsync(int taskId, string content, int userId)
    {
         await _taskRepository.AddCommentAsync(taskId, content, userId);
    }
    
    public async System.Threading.Tasks.Task UpdateTaskStatusAsync(int taskId, TaskStatus newStatus, int userId)
    {
        await _taskRepository.UpdateTaskStatusAsync(taskId, newStatus, userId);
    }


}