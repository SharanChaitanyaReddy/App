public class UnitOfWork : IUnitOfWork
{
    private readonly TaskDbContext _context;

    public UnitOfWork(TaskDbContext context)
    {
        _context = context;
        Tasks = new TaskRepository(_context);
        SubTasks = new SubTaskRepository(_context);
        Comments = new CommentRepository(_context);
    }

    public ITaskRepository Tasks { get; private set; }
    public ISubTaskRepository SubTasks { get; private set; }
    public ICommentRepository Comments { get; private set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
