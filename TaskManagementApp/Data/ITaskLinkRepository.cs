using TaskManagementApp.Models;

public interface ITaskLinkRepository
{
    Task<TaskLink> GetByIdAsync(int id);
    Task<IEnumerable<TaskLink>> GetAllByTaskIdAsync(int taskId);
    Task AddAsync(TaskLink taskLink);
    Task UpdateAsync(TaskLink taskLink);
    Task DeleteAsync(int id);
}
