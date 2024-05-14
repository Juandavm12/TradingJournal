//this class contains the methods of security for users (user,role, password)
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TradingJournal.API.Data;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.Entities;

namespace TradingJournal.API.Helpers
{
    public class UserHelper : IUserHelper
    {

        private readonly DataContext _context;
        private UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {

            return await _userManager.CreateAsync(user, password);

        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {

            await _userManager.CreateAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {

            bool roolExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roolExist)
            {

                await _roleManager.CreateAsync(new IdentityRole
                {

                    Name = roleName
                });

            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
