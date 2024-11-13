using TaskManagementApp.Models;

public class UserTaskHistoryService : IUserTaskHistoryService
{
    private readonly IUserTaskHistoryRepository _userTaskHistoryRepository;

    public UserTaskHistoryService(IUserTaskHistoryRepository userTaskHistoryRepository)
    {
        _userTaskHistoryRepository = userTaskHistoryRepository;
    }

    public async Task<UserTaskHistory> GetUserTaskHistoryByIdAsync(int id)
    {
        return await _userTaskHistoryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<UserTaskHistory>> GetUserTaskHistoryByTaskIdAsync(int taskId)
    {
        return await _userTaskHistoryRepository.GetAllByTaskIdAsync(taskId);
    }

    public async Task CreateUserTaskHistoryAsync(UserTaskHistory history)
    {
        await _userTaskHistoryRepository.AddAsync(history);
    }

    public async Task UpdateUserTaskHistoryAsync(UserTaskHistory history)
    {
        await _userTaskHistoryRepository.UpdateAsync(history);
    }

    public async Task DeleteUserTaskHistoryAsync(int id)
    {
        await _userTaskHistoryRepository.DeleteAsync(id);
    }
}
