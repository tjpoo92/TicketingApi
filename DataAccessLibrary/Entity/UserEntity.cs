namespace DataAccessLibrary.Entity;

public class UserEntity
{
	public int UserId { get; set; }
	public string? UserName { get; set; }
	public string? UserEmail { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
