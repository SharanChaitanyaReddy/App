using TaskManagementApp.Models;

public class SubTaskService : ISubTaskService
{
    private readonly ISubTaskRepository _subTaskRepository;

    // Constructor injection of the repository
    public SubTaskService(ISubTaskRepository subTaskRepository)
    {
        _subTaskRepository = subTaskRepository;
    }

    // Get a SubTask by its ID
    public async Task<SubTask> GetSubTaskByIdAsync(int id)
    {
        return await _subTaskRepository.GetByIdAsync(id);
    }

    // Get all SubTasks associated with a specific Task ID
    public async Task<IEnumerable<SubTask>> GetSubTasksByTaskIdAsync(int taskId)
    {
        return await _subTaskRepository.GetAllByTaskIdAsync(taskId);
    }

    // Create a new SubTask
    public async Task CreateSubTaskAsync(SubTask subTask)
    {
        // Business logic can be added here (e.g., validation, logging)
        await _subTaskRepository.AddAsync(subTask);
    }

    // Update an existing SubTask
    public async Task UpdateSubTaskAsync(SubTask subTask)
    {
        // Business logic for updating the subtask, such as validation checks
        await _subTaskRepository.UpdateAsync(subTask);
    }

    // Delete a SubTask by its ID
    public async Task DeleteSubTaskAsync(int id)
    {
        // Business logic for deleting a subtask (e.g., logging, auditing)
        await _subTaskRepository.DeleteAsync(id);
    }
}
