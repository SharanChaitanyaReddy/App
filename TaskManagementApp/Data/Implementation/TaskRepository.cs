using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> GetByIdAsync(int id)
    {
        return await _context.TaskItems
            .Include(t => t.SubTasks)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems
            .Include(t => t.SubTasks)
            .Include(t => t.Comments)
            .ToListAsync();
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.TaskItems.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.TaskItems.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
