public interface IUnitOfWork : IDisposable
{
    ITaskRepository Tasks { get; }
    ISubTaskRepository SubTasks { get; }
    ICommentRepository Comments { get; }
    Task<int> CompleteAsync();
}