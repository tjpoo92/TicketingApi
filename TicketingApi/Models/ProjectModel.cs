using DataAccessLibrary.Entity;

namespace TicketingApi.Models;

public class ProjectModel
{
	public int ProjectId { get; set; } // PK
    public int CreatedBy { get; set; } // FK users.UserId
    public DateTime? DateDue { get; set; }
	public DateTime? DateCompleted { get; set; }
	public string? ProjectName { get; set; }
	public string? ProjectDescription { get; set; }
	public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}