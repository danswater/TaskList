using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskList.Models;

namespace TaskList.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly TaskListDbContext _context;
    public UsersController(TaskListDbContext context)
    {
      _context = context;
    }

    /// <summary>
    ///   Get User List
    /// </summary>
    /// <returns>Returns User List</returns>
    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
      return _context.Users.ToList();
    }

    /// <summary>
    ///   Get a User item
    /// </summary>
    /// <returns>Returns User item</returns>
    /// <response code="200">Returns User item</response>
    /// <response code="404">If the item is null</response>
    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<User> GetById(long id)
    {
      var item = _context.Users.Find(id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    /// <summary>
    ///   Creates a User item
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /users
    ///     {
    ///        "firstName": "Item1",
    ///        "lastName": true,
    ///        "email": "John Doe"
    ///     }
    ///
    /// </remarks>
    /// <param name="item"></param>
    /// <returns>A newly created User item</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult Create(User item)
    {
      _context.Users.Add(item);
      _context.SaveChanges();

      return CreatedAtRoute("GetUser", new { id = item.Id }, item);
    }
  }
}