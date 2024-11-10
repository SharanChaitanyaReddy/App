public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public ICollection<Task> AssignedTasks { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<UserTaskHistory> TaskHistories { get; set; }
}
