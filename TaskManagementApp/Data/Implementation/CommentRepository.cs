using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class CommentRepository : ICommentRepository
{
    private readonly TaskDbContext _context;
    public CommentRepository (TaskDbContext dbContext){
        _context = dbContext;
    }

     public async Task<Comment> GetByIdAsync(int id)
    {
        return await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetAllByTaskIdAsync(int taskId)
    {
        return await _context.Comments
            .Where(c => c.TaskId == taskId)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}