using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/Comment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    // GET: api/Comment/task/{taskId}
    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByTaskId(int taskId)
    {
        var comments = await _commentService.GetCommentsByTaskIdAsync(taskId);
        if (comments == null || !comments.Any())
        {
            return NotFound();
        }
        return Ok(comments);
    }

    // POST: api/Comment
    [HttpPost]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment)
    {
        if (comment == null)
        {
            return BadRequest();
        }

        await _commentService.CreateCommentAsync(comment);
        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
    }

    // PUT: api/Comment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
    {
        if (id != comment.Id)
        {
            return BadRequest();
        }

        await _commentService.UpdateCommentAsync(comment);
        return NoContent();
    }

    // DELETE: api/Comment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}
