using DataAccessLibrary.Entity;

namespace DataAccessLibrary.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<UserEntity>> GetAllUsersAsync();
		Task<UserEntity?> GetUserByIdAsync(int id);
		Task CreateUserAsync(UserEntity user);
		Task UpdateUserAsync(UserEntity user);
		Task DeleteUserAsync(int id);
	}
}