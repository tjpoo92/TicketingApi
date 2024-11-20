namespace TicketingApi.Models;

public class TaskModel
{
	public int TaskId { get; set; } // PK
    public int ProjectId { get; set; } // FK projects.ProjectId
    public int CreatedBy { get; set; } // FK users.UserId
    public int AssignedTo { get; set; } // FK users.UserId
    public DateTime DateDue { get; set; }
    public DateTime DateCompleted { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
	public Status Status { get; set; }
	public Priority Priority { get; set; }
	public int PredessorTask { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
