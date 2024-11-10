public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<WorkflowStatusTransition> StatusTransitions { get; set; }
}

public class WorkflowStatusTransition
{
    public int Id { get; set; }
    public TaskStatus FromStatus { get; set; }
    public TaskStatus ToStatus { get; set; }
    public int WorkflowId { get; set; }
    public Workflow Workflow { get; set; }
}