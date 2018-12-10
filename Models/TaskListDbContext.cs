using Microsoft.EntityFrameworkCore;

namespace TaskList.Models
{
  public class TaskListDbContext : DbContext
  {
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options)
      : base(options)
    {
    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
  }
}