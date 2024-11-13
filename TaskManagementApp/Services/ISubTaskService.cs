using TaskManagementApp.Models;

public interface ISubTaskService
{
    Task<SubTask> GetSubTaskByIdAsync(int id);
    Task<IEnumerable<SubTask>> GetSubTasksByTaskIdAsync(int taskId);
    Task CreateSubTaskAsync(SubTask subTask);
    Task UpdateSubTaskAsync(SubTask subTask);
    Task DeleteSubTaskAsync(int id);
}
