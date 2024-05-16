//Interface of the Userhelper
using TradingJournal.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TradingJournal.Shared.DTOs;
using System;

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
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);

    }
}
