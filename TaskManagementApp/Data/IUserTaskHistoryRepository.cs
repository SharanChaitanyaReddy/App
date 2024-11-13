using TaskManagementApp.Models;

public interface IUserTaskHistoryRepository
{
    Task<UserTaskHistory> GetByIdAsync(int id);
    Task<IEnumerable<UserTaskHistory>> GetAllByTaskIdAsync(int taskId);
    Task AddAsync(UserTaskHistory history);
    Task UpdateAsync(UserTaskHistory history);
    Task DeleteAsync(int id);
}