using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using TicketingApi.Models;
using Priority = TicketingApi.Models.Priority;
using Status = TicketingApi.Models.Status;

public class UserService : IUserService {

    private readonly UserRepository _userRepository;
    private readonly Validator _validator;

    public UserService(UserRepository userRepository, Validator validator) {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        var usersFromDatabase= await _userRepository.GetAllUsersAsync();
        List<UserModel> users = [];
        foreach (var user in usersFromDatabase)
        {
            users.Add(CopyToModel(user));
        }
        return users;
    }

    // public async Task<UserModel> GetUserByIdAsync(int id)
    // {
    //     _validator.ValidateId(id, "User");
    //
    //     var user = await _userRepository.GetUserByIdAsync(id);
    //     _validator.ValidateObjectNotNull(user, "User");
    //     return user;
    // }
    
    public async Task CreateUserAsync(UserModel user)
    {
        _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.CreateUserAsync(CopyToEntity(user));
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.UpdateUserAsync(CopyToEntity(user));
    }

    // public async Task DeleteUserAsync(int id)
    // {
    //     _validator.ValidateId(id, "User");
    //
    //     var existingUser = await _userRepository.GetUserByIdAsync(id);
    //     _validator.ValidateObjectNotNull(existingUser, "User");
    //
    //     await _userRepository.DeleteUserAsync(id);
    // }
    
    private static UserModel CopyToModel(UserEntity from)
    {
        UserModel toModel = new UserModel
        {
            UserId = from.UserId,
            UserName = from.UserName,
            UserEmail = from.UserEmail,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toModel;
    }

    private static UserEntity CopyToEntity(UserModel from)
    {
        UserEntity toEntity = new UserEntity
        {
            UserId = from.UserId,
            UserName = from.UserName,
            UserEmail = from.UserEmail,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toEntity;
    }
}