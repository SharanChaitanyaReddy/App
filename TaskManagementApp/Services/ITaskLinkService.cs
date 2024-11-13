using TaskManagementApp.Models;

public interface ITaskLinkService
{
    Task<TaskLink> GetTaskLinkByIdAsync(int id);
    Task<IEnumerable<TaskLink>> GetTaskLinksByTaskIdAsync(int taskId);
    Task CreateTaskLinkAsync(TaskLink taskLink);
    Task UpdateTaskLinkAsync(TaskLink taskLink);
    Task DeleteTaskLinkAsync(int id);
}
