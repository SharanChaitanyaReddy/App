namespace TaskManagementApp.Models;
using System.ComponentModel.DataAnnotations;
public class Workflow
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required ICollection<WorkflowStatusTransition> StatusTransitions { get; set; }
}

public class WorkflowStatusTransition
{
    [Key]
    public int Id { get; set; }
    public TaskStatus FromStatus { get; set; }
    public TaskStatus ToStatus { get; set; }
    public int WorkflowId { get; set; }
    public required Workflow Workflow { get; set; }
}