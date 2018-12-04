namespace TaskList.Models
{
  public class Task
  {
    public long Id { get; set; }
    public string Body { get; set; }

    public bool IsComplete { get; set; }
  }
}