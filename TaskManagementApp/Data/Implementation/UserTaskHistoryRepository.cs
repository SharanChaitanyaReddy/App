using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class UserTaskHistoryRepository : IUserTaskHistoryRepository
{
    private readonly TaskDbContext _context;

    public UserTaskHistoryRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<UserTaskHistory> GetByIdAsync(int id)
    {
        return await _context.UserTaskHistories.FirstOrDefaultAsync(uth => uth.Id == id);
    }

    public async Task<IEnumerable<UserTaskHistory>> GetAllByTaskIdAsync(int taskId)
    {
        return await _context.UserTaskHistories.Where(uth => uth.TaskId == taskId).ToListAsync();
    }

    public async Task AddAsync(UserTaskHistory history)
    {
        await _context.UserTaskHistories.AddAsync(history);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserTaskHistory history)
    {
        _context.UserTaskHistories.Update(history);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var history = await GetByIdAsync(id);
        if (history != null)
        {
            _context.UserTaskHistories.Remove(history);
            await _context.SaveChangesAsync();
        }
    }
}