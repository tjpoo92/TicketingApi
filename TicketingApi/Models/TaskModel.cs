namespace TicketingApi.Models;

public class TaskModel
{
	public int task_id { get; set; }
    // TODO: Figure out FKs: project_id, created_by, assigned_to
    public DateTime date_due { get; set; }
    public DateTime date_completed { get; set; }
    public string task_name { get; set; }
    public string task_description { get; set; }
    // TODO: Create enums: status and priority
    public int predessor_task { get; set; }
	public DateTime created_at { get; set; }
	public DateTime updated_at { get; set; }
}
