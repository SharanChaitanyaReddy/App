using TaskManagementApp.Models;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentsRepository)
    {
        _commentRepository = commentsRepository;
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId)
    {
        return await _commentRepository.GetAllByTaskIdAsync(taskId);
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}