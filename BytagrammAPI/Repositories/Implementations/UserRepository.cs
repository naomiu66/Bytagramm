using BytagrammAPI.Data;
using BytagrammAPI.Models;
using BytagrammAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BytagrammAPI.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        UserManager<User> _userManager;
        public UserRepository(ApplicationContext context, UserManager<User> userManager) : base(context) 
        {
            _userManager = userManager;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string identifier)
        {
            var userByName = await _userManager.FindByNameAsync(identifier);
            if (userByName != null) return userByName;

            return await _userManager.FindByEmailAsync(identifier);
        }


    }
}