using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Model;

namespace MinimalAPIProject.Service;
public interface IUserService
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid id);
    public Task<User> CreateUser(CreateUserRequestDto createUserRequestDto);
    public Task<bool?> UpdateUser(Guid id, UpdateUserRequestDto updateUserRequestDto);
    public Task<bool> DeleteUser(Guid id);
}
