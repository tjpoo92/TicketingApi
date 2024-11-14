﻿namespace TicketingApi.Models;

public class UserModel
{
	public int UserId { get; set; }
	public string UserName { get; set; }
	public string UserEmail { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
