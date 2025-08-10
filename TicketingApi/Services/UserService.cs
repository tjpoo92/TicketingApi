using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using TicketingApi.Models;
using TicketingApi.Services.Interfaces;

namespace TicketingApi.Services;

public class UserService : IUserService {

    private readonly IUserRepository _userRepository;
    private readonly Validator _validator;
    private readonly AutoMapper.IMapper _mapper;

    public UserService(IUserRepository userRepository, Validator validator, AutoMapper.IMapper mapper) {
        _userRepository = userRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        var usersFromDatabase= await _userRepository.GetAllUsersAsync();
        List<UserModel> users = [];
        users.AddRange(usersFromDatabase.Select(user => _mapper.Map<UserModel>(user)));
        return users;
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        // _validator.ValidateId(id, "User");
    
        var userFromDatabase = await _userRepository.GetUserByIdAsync(id);
        // _validator.ValidateObjectNotNull(user, "User");
        return _mapper.Map<UserModel>(userFromDatabase);
    }
    
    public async Task CreateUserAsync(UserModel user)
    {
        // _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.CreateUserAsync(_mapper.Map<UserEntity>(user));
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        // _validator.ValidateObjectNotNull(user, "User");

        await _userRepository.UpdateUserAsync(_mapper.Map<UserEntity>(user));
    }

    public async Task DeleteUserAsync(int id)
    {
        // _validator.ValidateId(id, "User");
    
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        // _validator.ValidateObjectNotNull(existingUser, "User");
    
        await _userRepository.DeleteUserAsync(id);
    }
    
    private UserModel CopyToModel(UserEntity from) => _mapper.Map<UserModel>(from);
    private UserEntity CopyToEntity(UserModel from) => _mapper.Map<UserEntity>(from);
}