namespace TicketingApi.Models;

public class ProjectModel
{
	public int project_id { get; set; }
	// TODO: Figure out FKs: created_by
	public DateTime date_due { get; set; }
	public DateTime date_completed { get; set; }
	public string project_name { get; set; }
	public string project_description { get; set; }
	// TODO: Create enums: status and priority
	public DateTime created_at { get; set; }
	public DateTime updated_at { get; set; }
}