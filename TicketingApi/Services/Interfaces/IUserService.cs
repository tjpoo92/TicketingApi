
using TicketingApi.Models;

namespace TicketingApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
    Task CreateUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel user);
    Task DeleteUserAsync(int id);
}