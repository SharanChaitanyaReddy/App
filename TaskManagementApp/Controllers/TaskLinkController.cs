using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

[Route("api/[controller]")]
[ApiController]
public class TaskLinkController : ControllerBase
{
    private readonly ITaskLinkService _taskLinkService;

    public TaskLinkController(ITaskLinkService taskLinkService)
    {
        _taskLinkService = taskLinkService;
    }

    // GET: api/TaskLink/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskLink>> GetTaskLinkById(int id)
    {
        var taskLink = await _taskLinkService.GetTaskLinkByIdAsync(id);
        if (taskLink == null)
        {
            return NotFound();
        }
        return Ok(taskLink);
    }

    // GET: api/TaskLink/task/{taskId}
    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<TaskLink>>> GetTaskLinksByTaskId(int taskId)
    {
        var taskLinks = await _taskLinkService.GetTaskLinksByTaskIdAsync(taskId);
        if (taskLinks == null || !taskLinks.Any())
        {
            return NotFound();
        }
        return Ok(taskLinks);
    }

    // POST: api/TaskLink
    [HttpPost]
    public async Task<ActionResult<TaskLink>> CreateTaskLink([FromBody] TaskLink taskLink)
    {
        if (taskLink == null)
        {
            return BadRequest();
        }

        await _taskLinkService.CreateTaskLinkAsync(taskLink);
        return CreatedAtAction(nameof(GetTaskLinkById), new { id = taskLink.Id }, taskLink);
    }

    // PUT: api/TaskLink/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskLink(int id, [FromBody] TaskLink taskLink)
    {
        if (id != taskLink.Id)
        {
            return BadRequest();
        }

        await _taskLinkService.UpdateTaskLinkAsync(taskLink);
        return NoContent();
    }

    // DELETE: api/TaskLink/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskLink(int id)
    {
        await _taskLinkService.DeleteTaskLinkAsync(id);
        return NoContent();
    }
}
