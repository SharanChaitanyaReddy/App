using System.Collections.Generic;
using TaskManagementApp.Models;

public interface ITaskService
{
    Task<TaskItem> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task CreateTaskAsync(TaskItem task);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int id);
}
