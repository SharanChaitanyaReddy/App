using TaskManagementApp.Models;

public class TaskLinkService : ITaskLinkService
{
    private readonly ITaskLinkRepository _taskLinkRepository;

    public TaskLinkService(ITaskLinkRepository taskLinkRepository)
    {
        _taskLinkRepository = taskLinkRepository;
    }

    public async Task<TaskLink> GetTaskLinkByIdAsync(int id)
    {
        return await _taskLinkRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TaskLink>> GetTaskLinksByTaskIdAsync(int taskId)
    {
        return await _taskLinkRepository.GetAllByTaskIdAsync(taskId);
    }

    public async Task CreateTaskLinkAsync(TaskLink taskLink)
    {
        await _taskLinkRepository.AddAsync(taskLink);
    }

    public async Task UpdateTaskLinkAsync(TaskLink taskLink)
    {
        await _taskLinkRepository.UpdateAsync(taskLink);
    }

    public async Task DeleteTaskLinkAsync(int id)
    {
        await _taskLinkRepository.DeleteAsync(id);
    }
}
