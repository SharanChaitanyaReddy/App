using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class SubTaskRepository: ISubTaskRepository
{
    private readonly TaskDbContext _context;
    public SubTaskRepository (TaskDbContext dbContext){
        _context = dbContext;
    }

    public async Task<SubTask> GetByIdAsync(int id)
    {
        return await _context.SubTasks.FirstOrDefaultAsync(st => st.Id == id);
    }

    public async Task<IEnumerable<SubTask>> GetAllByTaskIdAsync(int taskId)
    {
        return await _context.SubTasks.Where(st => st.ParentTaskId == taskId).ToListAsync();
    }

    public async Task AddAsync(SubTask subTask)
    {
        await _context.SubTasks.AddAsync(subTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SubTask subTask)
    {
        _context.SubTasks.Update(subTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var subTask = await GetByIdAsync(id);
        if (subTask != null)
        {
            _context.SubTasks.Remove(subTask);
            await _context.SaveChangesAsync();
        }
    }
}