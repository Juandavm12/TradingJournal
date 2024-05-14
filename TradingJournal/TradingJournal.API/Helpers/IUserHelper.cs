//Interface of the Userhelper
using TradingJournal.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TradingJournal.API.Helpers
{
    public interface IUserHelper
    {
        //define the methods of userhelper
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }
}
