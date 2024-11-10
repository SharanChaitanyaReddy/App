namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
public class UserTaskHistory
{
    [Key]
    public int Id { get; set; }
    public int TaskId { get; set; }
    public TaskItem? Task { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public TaskStatus OldStatus { get; set; }
    public TaskStatus NewStatus { get; set; }
    public DateTime ChangedAt { get; set; }
}