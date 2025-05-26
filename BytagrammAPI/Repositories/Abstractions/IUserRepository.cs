using BytagrammAPI.Models;

namespace BytagrammAPI.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameOrEmailAsync(string identifier);

        Task<User?> GetByUsernameAsync(string username);
    }
}