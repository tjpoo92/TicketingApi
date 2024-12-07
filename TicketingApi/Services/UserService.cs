using TicketingApi.Models;

public class UserService : IUserService {

    private readonly IUserRepository _userRepository;
    private readonly Validator _validator;

    public UserService(IUserRepository userRepository, Validator validator) {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        _validator.ValidateId(id, "User");

        var user = await _userRepository.GetUserByIdAsync(id);
        _validator.ValidateObjectNotNull(user, "User");
        return user;
    }
    
    public async Task CreateUserAsync(UserModel user)
    {
        _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.CreateUserAsync(user);
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        _validator.ValidateId(id, "User");

        var existingUser = await _userRepository.GetUserByIdAsync(id);
        _validator.ValidateObjectNotNull(existingUser, "User");

        await _userRepository.DeleteUserAsync(id);
    }
}