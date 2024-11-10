namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
public class SubTask
{
    [Key]
    public int Id { get; set; }
    public required string Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    
    // Foreign key relationship
    public int ParentTaskId { get; set; }
    public required TaskItem ParentTask { get; set; }
}