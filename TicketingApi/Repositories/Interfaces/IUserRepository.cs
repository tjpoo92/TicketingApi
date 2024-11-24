using TicketingApi.Models;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetUserByIdAsync(int id);
    Task<UserModel> CreateUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel user);
    Task DeleteUserAsync(int id);
}