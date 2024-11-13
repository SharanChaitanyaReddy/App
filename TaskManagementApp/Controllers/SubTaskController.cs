using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

[Route("api/[controller]")]
[ApiController]
public class SubTaskController : ControllerBase
{
    private readonly ISubTaskService _subTaskService;

    public SubTaskController(ISubTaskService subTaskService)
    {
        _subTaskService = subTaskService;
    }

    // GET: api/SubTask/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SubTask>> GetSubTaskById(int id)
    {
        var subTask = await _subTaskService.GetSubTaskByIdAsync(id);
        if (subTask == null)
        {
            return NotFound();
        }
        return Ok(subTask);
    }

    // GET: api/SubTask/task/{taskId}
    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<SubTask>>> GetSubTasksByTaskId(int taskId)
    {
        var subTasks = await _subTaskService.GetSubTasksByTaskIdAsync(taskId);
        if (subTasks == null || !subTasks.Any())
        {
            return NotFound();
        }
        return Ok(subTasks);
    }

    // POST: api/SubTask
    [HttpPost]
    public async Task<ActionResult<SubTask>> CreateSubTask([FromBody] SubTask subTask)
    {
        if (subTask == null)
        {
            return BadRequest();
        }

        await _subTaskService.CreateSubTaskAsync(subTask);
        return CreatedAtAction(nameof(GetSubTaskById), new { id = subTask.Id }, subTask);
    }

    // PUT: api/SubTask/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubTask(int id, [FromBody] SubTask subTask)
    {
        if (id != subTask.Id)
        {
            return BadRequest();
        }

        await _subTaskService.UpdateSubTaskAsync(subTask);
        return NoContent();
    }

    // DELETE: api/SubTask/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubTask(int id)
    {
        await _subTaskService.DeleteSubTaskAsync(id);
        return NoContent();
    }
}
