using DataAccessLibrary.Entity;
using System.ComponentModel.DataAnnotations;

namespace TicketingApi.Models;

public class ProjectModel
{
	public int ProjectId { get; set; } // PK
    
    [Required]
    public int CreatedBy { get; set; } // FK users.UserId
    
    public DateTime? DateDue { get; set; }
	public DateTime? DateCompleted { get; set; }
	
	[Required]
	[StringLength(100, MinimumLength = 1)]
	public string? ProjectName { get; set; }
	
	[StringLength(500)]
	public string? ProjectDescription { get; set; }
	
	[Required]
	public Status Status { get; set; }
    
    [Required]
    public Priority Priority { get; set; }
    
    public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}