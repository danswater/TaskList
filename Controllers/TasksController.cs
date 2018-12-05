using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskList.Models;

namespace TaskList.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private readonly TaskListDbContext _context;
    public TasksController(TaskListDbContext context)
    {
      _context = context;
    }

    /// <summary>
    ///   Get Task List
    /// </summary>
    /// <returns>Returns Task item</returns>
    [HttpGet]
    public ActionResult<List<Task>> GetAll()
    {
      return _context.Tasks.ToList();
    }

    /// <summary>
    ///   Get a Task item
    /// </summary>
    /// <returns>Returns Task item</returns>
    /// <response code="200">Returns Task item</response>
    /// <response code="404">If the item is null</response>
    [HttpGet("{id}", Name = "GetTodo")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<Task> GetById(long id)
    {
      var item = _context.Tasks.Find(id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    /// <summary>
    ///   Creates a Task item
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "name": "Item1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <param name="item"></param>
    /// <returns>A newly created Task item</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(Task item)
    {
      _context.Tasks.Add(item);
      _context.SaveChanges();

      return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }

    /// <summary>
    ///   Update a Task item
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Todo/1
    ///     {
    ///        "name": "Item1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <returns>Currently returns empty response</returns>
    /// <response code="200">Returns empty response</response>
    /// <response code="404">If the item is null</response>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult Update(long id, Task item)
    {
      var task = _context.Tasks.Find(id);
      if (task == null)
      {
        return NotFound();
      }

      task.Body = item.Body;
      task.IsComplete = item.IsComplete;

      _context.Tasks.Update(task);
      _context.SaveChanges();
      return NoContent();
    }

    /// <summary>
    /// Deletes a specific Task item
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var task = _context.Tasks.Find(id);
      if (task == null)
      {
        return NotFound();
      }

      _context.Tasks.Remove(task);
      _context.SaveChanges();
      return NoContent();
    }
  }
}