using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

[Route("api/[controller]")]
[ApiController]
public class TaskApiController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskApiController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetAllTasks() => Ok(_taskService.GetAllTasks());

    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id) => Ok(_taskService.GetTaskById(id));

    [HttpPost]
    public IActionResult AddTask(Task task) { _taskService.AddTask(task); return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task); }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, Task task) { task.Id = id; _taskService.UpdateTask(task); return NoContent(); }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id) { _taskService.DeleteTask(id); return NoContent(); }
}
