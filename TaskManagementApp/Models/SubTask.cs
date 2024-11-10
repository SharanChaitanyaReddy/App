public class SubTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    
    // Foreign key relationship
    public int TaskId { get; set; }
    public Task Task { get; set; }
}