using TaskManagementApp.Models;
using System.Collections.Generic;

public interface ITaskService
{
    List<Task> GetAllTasks();
    Task GetTaskById(int id);
    void AddTask(Task task);
    void UpdateTask(Task task);
    void DeleteTask(int id);
}
