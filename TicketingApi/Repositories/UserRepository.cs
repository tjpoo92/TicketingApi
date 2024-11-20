using TicketingApi.Models;

public class UserRepository : IUserRepository
{
    public Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        throw new NotImplementedException();
    }
    
    public Task<UserModel> CreateUserAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}