using System.Collections.Generic;
using TaskManagementApp.Models;

public interface ITaskService
{
    List<TaskItem> GetAllTasks();
    Task GetTaskById(int id);
    void AddTask(TaskItem task);
    void UpdateTask(TaskItem task);
    void DeleteTask(int id);
}
