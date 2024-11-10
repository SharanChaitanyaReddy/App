using Microsoft.EntityFrameworkCore;

public class TaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async System.Threading.Tasks.Task<TaskItem> GetTaskByIdAsync(int taskId)
    {
        return await _context.Tasks
            .Include(t => t.SubTasks)
            .Include(t => t.TaskLinks)
            .Include(t => t.Comments)
            .Include(t => t.TaskHistories)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async System.Threading.Tasks.Task UpdateTaskStatusAsync(int taskId, TaskStatus newStatus, int userId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            var oldStatus = task.Status;
            task.Status = newStatus;

            var history = new UserTaskHistory
            {
                TaskId = taskId,
                UserId = userId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedAt = DateTime.UtcNow
            };
            _context.UserTaskHistories.Add(history);

            await _context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task AddCommentAsync(int taskId, string content, int userId)
    {
        var comment = new Comment
        {
            TaskId = taskId,
            Content = content,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }
}
