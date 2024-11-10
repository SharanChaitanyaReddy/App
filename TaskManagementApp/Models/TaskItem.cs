namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;

public class TaskItem
{

    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TaskPriority Priority { get; set; } // Enum: Low, Medium, High
    public TaskStatus Status { get; set; }     // Enum: ToDo, InProgress, Done
    public DateTime DueDate { get; set; }
    
    // Foreign key relationships
    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
    public ICollection<SubTask>? SubTasks { get; set; }
    public ICollection<TaskLink>? TaskLinks { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<UserTaskHistory>? UserTaskHistories { get; set; }
    public DateTime ExpiryDate { get; set; }
}