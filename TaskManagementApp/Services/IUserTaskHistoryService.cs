using TaskManagementApp.Models;

public interface IUserTaskHistoryService
{
    Task<UserTaskHistory> GetUserTaskHistoryByIdAsync(int id);
    Task<IEnumerable<UserTaskHistory>> GetUserTaskHistoryByTaskIdAsync(int taskId);
    Task CreateUserTaskHistoryAsync(UserTaskHistory history);
    Task UpdateUserTaskHistoryAsync(UserTaskHistory history);
    Task DeleteUserTaskHistoryAsync(int id);
}
