public class UserTaskHistory
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public Task Task { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public TaskStatus OldStatus { get; set; }
    public TaskStatus NewStatus { get; set; }
    public DateTime ChangedAt { get; set; }
}