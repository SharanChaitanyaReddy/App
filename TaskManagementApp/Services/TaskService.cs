using TaskManagementApp.Data;
using TaskManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
public class TaskService : ITaskService
{
 private readonly TaskDbContext _context;

    public TaskService(TaskDbContext context)
    {
        _context = context;
    }

    public List<Task> GetAllTasks() => _context.Tasks.ToList();
    public Task GetTaskById(int id) => _context.Tasks.Find(id);
    public void AddTask(Task task) { _context.Tasks.Add(task); _context.SaveChanges(); }
    public void UpdateTask(Task task) { _context.Tasks.Update(task); _context.SaveChanges(); }
    public void DeleteTask(int id) { var task = GetTaskById(id); _context.Tasks.Remove(task); _context.SaveChanges(); }

}