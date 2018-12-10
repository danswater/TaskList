using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
  public class User
  {
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
  }
}