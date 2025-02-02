using Dapper;
using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly string _connectionString;
		private SqlDataAccess db = new SqlDataAccess();

		public UserRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");;
		}

		public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
		{
			string sql = "SELECT user_id as UserId, user_email as UserEmail, user_name as UserName, created_at as CreatedAt, updated_at as UpdatedAt FROM dbo.users";
			return await db.LoadDataAsync<UserEntity, dynamic>(sql, new { }, _connectionString);
		}

		// public async Task<UserEntity> GetUserByIdAsync(int id)
		// {
		// 	string sql = "SELECT * FROM dbo.users WHERE user_id = @Id";
		// 	List<UserEntity> output = await db.LoadDataAsync<UserEntity, dynamic>(sql, new { Id = id }, _connectionString);
		// 	return output.First();
		// }

		public async Task CreateUserAsync(UserEntity user)
		{
			string sql = "INSERT INTO dbo.users (user_name, user_email) VALUES (@UserName, @UserEmail)";
			await db.SaveDataAsync(sql, new { user.UserName, user.UserEmail }, _connectionString);
			return;
		}

		public async Task UpdateUserAsync(UserEntity user)
		{
			string sql = string.Empty;
			if (user.UserEmail == null)
			{
				sql = "UPDATE dbo.users SET user_name = @UserName WHERE user_id = @Id";
				await db.SaveDataAsync(sql, new { user.UserName, user.UserId }, _connectionString);
				return;
			}
			else if (user.UserName == null)
			{
				sql = "UPDATE dbo.users SET user_email = @UserEmail WHERE user_id = @Id";
				await db.SaveDataAsync(sql, new { user.UserEmail, user.UserId }, _connectionString);
				return;
			}
			else
			{
				sql = "UPDATE dbo.users SET user_name = @UserName, user_email = @UserEmail WHERE user_id = @Id";
				await db.SaveDataAsync(sql, new { user.UserName, user.UserEmail }, _connectionString);
				return;
			}
		}

		public async Task DeleteUserAsync(int id)
		{
			string sql = "DELETE FROM dbo.users WHERE user_id = @Id";
			await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
			return;
		}
	}
}