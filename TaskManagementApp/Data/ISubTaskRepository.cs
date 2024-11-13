
using TaskManagementApp.Models;

public interface ISubTaskRepository
{
    Task<SubTask> GetByIdAsync(int id);
    Task<IEnumerable<SubTask>> GetAllByTaskIdAsync(int taskId);
    Task AddAsync(SubTask subTask);
    Task UpdateAsync(SubTask subTask);
    Task DeleteAsync(int id);

}