using MinimalAPIProject.Model;

namespace MinimalAPIProject.Repository;
public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid id);
    public Task<User> CreateUser(User user);
    public Task<bool?> UpdateUser(User user);
    public Task<bool> DeleteUser(Guid id);
}
