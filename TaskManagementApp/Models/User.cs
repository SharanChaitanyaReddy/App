namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required ICollection<Task> AssignedTasks { get; set; }
    public required ICollection<Comment> Comments { get; set; }
    public required ICollection<UserTaskHistory> TaskHistories { get; set; }
}
