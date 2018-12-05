using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
  public class Task
  {
    public long Id { get; set; }

    [Required]
    public string Body { get; set; }
    public bool IsComplete { get; set; }
  }
}