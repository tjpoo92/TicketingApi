using TicketingApi.Models;

public class UserServiceValidator {
    // Validator for each method
    // Valid integer checks
    // Validate response objects aren't null
    // Validate any required fields


    private readonly IUserRepository _userRepository;

    public UserServiceValidator(IUserRepository userRepository) {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }


}

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;
    private readonly UserServiceValidator _validator;

    public UserService(IUserRepository userRepository, UserServiceValidator validator) {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }
    
    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(user.UserId);
        if (existingUser == null) {
            throw new KeyNotFoundException("User not found");
        }
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null) {
            throw new KeyNotFoundException("User not found");
        }
        await _userRepository.DeleteUserAsync(id);
    }
}