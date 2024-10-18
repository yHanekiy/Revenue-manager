using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Model;

namespace Project.Repository;

public class UserRepository(APBDContext apbdContext) : IUserRepository
{
    public async Task RegisterUser(User user)
    {
        await apbdContext.Users.AddAsync(user);
        await apbdContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        return await apbdContext.Users.Where(x => x.Login == login).FirstOrDefaultAsync();
    }
}