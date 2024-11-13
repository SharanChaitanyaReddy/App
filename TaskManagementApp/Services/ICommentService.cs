using TaskManagementApp.Models;

public interface ICommentService
{
    Task<Comment> GetCommentByIdAsync(int id);
    Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId);
    Task CreateCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);

}