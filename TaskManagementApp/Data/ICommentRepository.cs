using TaskManagementApp.Models;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllByTaskIdAsync(int taskId);
    Task AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);

}