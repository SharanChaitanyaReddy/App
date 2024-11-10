public class TaskLink
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public TaskItem Task { get; set; }
    
    public int LinkedTaskId { get; set; }
    public TaskItem LinkedTask { get; set; }
    
    public TaskLinkType LinkType { get; set; } // Enum: BlockedBy, RelatesTo, Duplicates
}