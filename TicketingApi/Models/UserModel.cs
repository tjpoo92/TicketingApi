namespace TicketingApi.Models;

public class UserModel
{
	public int user_id { get; set; }
	public string user_name { get; set; }
	public string user_email { get; set; }
	public DateTime created_at { get; set; }
	public DateTime updated_at { get; set; }
}
