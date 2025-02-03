using System.Reflection;
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

		public async Task<UserEntity> GetUserByIdAsync(int id)
		{
			string sql = "SELECT user_id as UserId, user_email as UserEmail, user_name as UserName, created_at as CreatedAt, updated_at as UpdatedAt FROM dbo.users WHERE user_id = @Id";
			IEnumerable<UserEntity> output = await db.LoadDataAsync<UserEntity, dynamic>(sql, new { Id = id }, _connectionString);
			return output.First();
		}

		public async Task CreateUserAsync(UserEntity user)
		{
			user.CreatedAt = DateTime.UtcNow;
			user.UpdatedAt = DateTime.UtcNow;
			
			string sql = "INSERT INTO dbo.users (user_name, user_email, created_at, updated_at) VALUES (@UserName, @UserEmail, @CreatedAt, @UpdatedAt)";
			await db.SaveDataAsync(sql, user, _connectionString);
		}

		public async Task UpdateUserAsync(UserEntity user)
		{
			UserEntity oldUser = await GetUserByIdAsync(user.UserId);
			if (oldUser == null)
			{
				await CreateUserAsync(user);
			}
			else
			{
				UserEntity updateUser = new()
				{
					UserId = user.UserId,
					UserEmail = user.UserEmail ?? oldUser.UserEmail,
					UserName = user.UserName ?? oldUser.UserName,
					UpdatedAt = DateTime.UtcNow
				};
				string sql = "UPDATE dbo.users SET user_email = @UserEmail, user_name = @UserName, updated_at = @UpdatedAt WHERE user_id = @UserId";
				await db.SaveDataAsync(sql, updateUser, _connectionString);
			}
		}

		public async Task DeleteUserAsync(int id)
		{
			string sql = "DELETE FROM dbo.users WHERE user_id = @Id";
			await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
		}
	}
}