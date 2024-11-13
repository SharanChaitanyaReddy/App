using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

[Route("api/[controller]")]
[ApiController]
public class UserTaskHistoryController : ControllerBase
{
    private readonly IUserTaskHistoryService _userTaskHistoryService;

    public UserTaskHistoryController(IUserTaskHistoryService userTaskHistoryService)
    {
        _userTaskHistoryService = userTaskHistoryService;
    }

    // GET: api/UserTaskHistory/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserTaskHistory>> GetUserTaskHistoryById(int id)
    {
        var history = await _userTaskHistoryService.GetUserTaskHistoryByIdAsync(id);
        if (history == null)
        {
            return NotFound();
        }

        return Ok(history);
    }

    // GET: api/UserTaskHistory/task/{taskId}
    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<UserTaskHistory>>> GetUserTaskHistoryByTaskId(int taskId)
    {
        var histories = await _userTaskHistoryService.GetUserTaskHistoryByTaskIdAsync(taskId);
        if (histories == null || !histories.Any())
        {
            return NotFound();
        }

        return Ok(histories);
    }

    // POST: api/UserTaskHistory
    [HttpPost]
    public async Task<ActionResult<UserTaskHistory>> CreateUserTaskHistory([FromBody] UserTaskHistory history)
    {
        if (history == null)
        {
            return BadRequest();
        }

        await _userTaskHistoryService.CreateUserTaskHistoryAsync(history);
        return CreatedAtAction(nameof(GetUserTaskHistoryById), new { id = history.Id }, history);
    }

    // PUT: api/UserTaskHistory/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserTaskHistory(int id, [FromBody] UserTaskHistory history)
    {
        if (id != history.Id)
        {
            return BadRequest();
        }

        await _userTaskHistoryService.UpdateUserTaskHistoryAsync(history);
        return NoContent();
    }

    // DELETE: api/UserTaskHistory/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserTaskHistory(int id)
    {
        await _userTaskHistoryService.DeleteUserTaskHistoryAsync(id);
        return NoContent();
    }
}
