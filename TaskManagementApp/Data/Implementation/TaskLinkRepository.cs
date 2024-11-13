using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class TaskLinkRepository : ITaskLinkRepository
{
    private readonly TaskDbContext _context;

    public TaskLinkRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskLink> GetByIdAsync(int id)
    {
        return await _context.TaskLinks
                             .Include(tl => tl.TaskItem)  // Assuming the TaskLink has a reference to the Task entity
                             .FirstOrDefaultAsync(tl => tl.Id == id);
    }

    public async Task<IEnumerable<TaskLink>> GetAllByTaskIdAsync(int taskId)
    {
        return await _context.TaskLinks
                             .Where(tl => tl.TaskId == taskId)
                             .ToListAsync();
    }

    public async Task AddAsync(TaskLink taskLink)
    {
        _context.TaskLinks.Add(taskLink);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskLink taskLink)
    {
        _context.TaskLinks.Update(taskLink);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var taskLink = await GetByIdAsync(id);
        if (taskLink != null)
        {
            _context.TaskLinks.Remove(taskLink);
            await _context.SaveChangesAsync();
        }
    }
}
