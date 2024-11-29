using TicketingApi;
using TicketingApi.Models;
using System.Collections.Generic;
using System.Linq;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    private SqlDataAccess db = new SqlDataAccess();

	public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        string sql = "SELECT * FROM dbo.users";

		return await db.LoadDataAsync<UserModel, dynamic>(sql, new { }, _connectionString);
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        string sql = "SELECT * FROM dbo.users WHERE user_id = @Id";
        List<UserModel> output = await db.LoadDataAsync<UserModel, dynamic>(sql, new { Id = id }, _connectionString);
		return output.First();
    }

	public async Task CreateUserAsync(UserModel user)
    {
        string sql = "INSERT INTO dbo.users (user_name, user_email) VALUES (@UserName, @UserEmail)";

		await db.SaveDataAsync(sql, new { user.UserName, user.UserEmail }, _connectionString);
    }

	public async Task UpdateUserAsync(UserModel user)
    {
        string sql = String.Empty;
        if (user.UserEmail == null)
        {
            sql = "UPDATE dbo.users SET user_name = @UserName WHERE user_id = @Id";
            await db.SaveDataAsync(sql, new { user.UserName, user.UserId }, _connectionString);
        }
        else if (user.UserName == null)
        {
            sql = "UPDATE dbo.users SET user_email = @UserEmail WHERE user_id = @Id";
            await db.SaveDataAsync(sql, new { user.UserEmail, user.UserId }, _connectionString);
		}
        else
        {
			sql = "UPDATE dbo.users SET user_name = @UserName, user_email = @UserEmail WHERE user_id = @Id";
            await db.SaveDataAsync(sql, new { user.UserName, user.UserEmail }, _connectionString);
		}
    }

    public async Task DeleteUserAsync(int id)
    {
        string sql = "DELETE FROM dbo.users WHERE user_id = @Id";
        await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
    }
}