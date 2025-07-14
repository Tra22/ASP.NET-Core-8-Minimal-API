using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Model;
using MinimalAPIProject.Repository;

namespace MinimalAPIProject.Service;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }
    public async Task<User> CreateUser(CreateUserRequestDto createUserRequestDto)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = createUserRequestDto.Username,
            Email = createUserRequestDto.Email,
            Password = createUserRequestDto.Password,
            CreatedDate = DateTime.Now
        };
        return await _userRepository.CreateUser(user);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        return await _userRepository.DeleteUser(id);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<bool?> UpdateUser(Guid id, UpdateUserRequestDto updateUserRequestDto)
    {
        User user = new User
        {
            Id = id,
            Username = updateUserRequestDto.Username,
            Email = updateUserRequestDto.Email,
            Password = updateUserRequestDto.Password,
            CreatedDate = DateTime.Now
        };
        return await _userRepository.UpdateUser(user);
    }
}
