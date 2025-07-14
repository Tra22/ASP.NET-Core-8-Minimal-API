using MinimalAPIProject.Mock_Data;
using MinimalAPIProject.Model;

namespace MinimalAPIProject.Repository;
public class UserRepository : IUserRepository
{

    public async Task<User> CreateUser(User user)
    {
        UserMock.users = UserMock.users.Append(user);
        return await Task.Run(() => user);
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        User? user = UserMock.users.FirstOrDefault(x => x.Id == id);
        if(user is null) return await Task.Run(() => false);
        UserMock.users = UserMock.users.Where(x => x.Id != id);
        return await Task.Run(() => true);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await Task.Run(() => UserMock.users);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await Task.Run(() => UserMock.users.FirstOrDefault(x => x.Id == id));
    }

    public async Task<bool?> UpdateUser(User user)
    {
        User? user_ = UserMock.users.FirstOrDefault(x => x.Id == user.Id);
        if(user_ is null) return null;
        user_ = user;
        UserMock.users = UserMock.users.Select(usr => {
            if(usr.Id == user_.Id)
                return user_;
            else return usr;
        });
        return await Task.Run(() => true);
    }
}
