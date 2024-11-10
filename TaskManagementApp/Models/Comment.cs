public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Foreign key relationships
    public int UserId { get; set; }
    public User User { get; set; }
    public int TaskId { get; set; }
    public Task Task { get; set; }
}