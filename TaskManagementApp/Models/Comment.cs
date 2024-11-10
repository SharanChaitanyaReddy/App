namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Foreign key relationships
    public int UserId { get; set; }
    public User? User { get; set; }
    public int TaskId { get; set; }
    public TaskItem? Task { get; set; }
}