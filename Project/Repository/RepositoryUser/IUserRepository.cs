using Project.Model;

namespace Project.Repository;

public interface IUserRepository
{
    public Task RegisterUser(User user);
    public Task<User?> GetUserByLogin(string login);
}