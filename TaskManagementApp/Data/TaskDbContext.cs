using Microsoft.EntityFrameworkCore;

public class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
    public DbSet<TaskLink> TaskLinks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserTaskHistory> UserTaskHistories { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<WorkflowStatusTransition> WorkflowStatusTransitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskLink>()
            .HasOne(t => t.Task)
            .WithMany(t => t.TaskLinks)
            .HasForeignKey(t => t.TaskId);

        modelBuilder.Entity<TaskLink>()
            .HasOne(t => t.LinkedTask)
            .WithMany()
            .HasForeignKey(t => t.LinkedTaskId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}