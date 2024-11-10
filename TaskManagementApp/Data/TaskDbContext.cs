using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
    public DbSet<TaskLink> TaskLinks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserTaskHistory> UserTaskHistories { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<WorkflowStatusTransition> WorkflowStatusTransitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaskItem>().HasKey(t => t.Id);

        // TaskItem -> SubTasks relationship
        modelBuilder.Entity<SubTask>()
            .HasOne(st => st.ParentTask)
            .WithMany(t => t.SubTasks)
            .HasForeignKey(st => st.ParentTaskId);

        // TaskItem -> TaskLinks relationship (to link tasks)
        modelBuilder.Entity<TaskLink>()
            .HasOne(t => t.Task)
            .WithMany(t => t.TaskLinks)
            .HasForeignKey(t => t.TaskId);

        modelBuilder.Entity<TaskLink>()
            .HasOne(t => t.LinkedTask)
            .WithMany()
            .HasForeignKey(t => t.LinkedTaskId);

        // TaskItem -> Comments relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId);

        // TaskItem -> UserTaskHistory relationship
        modelBuilder.Entity<UserTaskHistory>()
            .HasOne(uth => uth.Task)
            .WithMany(t => t.UserTaskHistories)
            .HasForeignKey(uth => uth.TaskId);

        // User -> UserTaskHistory relationship
        modelBuilder.Entity<UserTaskHistory>()
            .HasOne(uth => uth.User)
            .WithMany(u => u.TaskHistories)
            .HasForeignKey(uth => uth.TaskId);

        // Seed data for Status
        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "New" },
            new Status { Id = 2, Name = "In Progress" },
            new Status { Id = 3, Name = "Completed" },
            new Status { Id = 4, Name = "On Hold" }
        );
    }
}