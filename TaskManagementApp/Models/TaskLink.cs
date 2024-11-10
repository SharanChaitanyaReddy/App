namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
public class TaskLink
{
    [Key]
    public int Id { get; set; }
    public int TaskId { get; set; }
    public required TaskItem Task { get; set; }
    
    public int LinkedTaskId { get; set; }
    public required TaskItem LinkedTask { get; set; }
    
    public TaskLinkType LinkType { get; set; } // Enum: BlockedBy, RelatesTo, Duplicates
}