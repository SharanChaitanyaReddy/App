using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Models;
public class TaskService : ITaskService
{
 private readonly TaskRepository _taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

   public async Task<TaskItem> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task CreateTaskAsync(TaskItem task)
    {
        // Additional business logic can be applied here
        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        // Additional validation or logic
        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteAsync(id);
    }


}