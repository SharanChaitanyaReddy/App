using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskApiController : ControllerBase
{
   private readonly ITaskService _taskService;

    public TaskApiController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskItem task)
    {
        await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }
}
