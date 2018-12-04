using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskList.Models;

namespace TaskList.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private readonly TaskListDbContext _context;
    public TasksController(TaskListDbContext context)
    {
      _context = context;

      if (_context.Tasks.Count() == 0)
      {
        _context.Tasks.Add(new Task { Body = "Lorem Ipsum dolor saosin taking back sunday" });
        _context.SaveChanges();
      }
    }

    [HttpGet]
    public ActionResult<List<Task>> GetAll()
    {
      return _context.Tasks.ToList();
    }

    [HttpGet("{id}", Name = "GetTodo")]
    public ActionResult<Task> GetById(long id)
    {
      var item = _context.Tasks.Find(id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    [HttpPost]
    public IActionResult Create(Task item)
    {
      _context.Tasks.Add(item);
      _context.SaveChanges();

      return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
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