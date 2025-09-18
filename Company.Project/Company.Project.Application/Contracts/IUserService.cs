using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Company.Project.Application.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserProfileAsync(string userId);
        Task<bool> UpdateUserProfileAsync(string userId, string? fullName, string? phoneNumber);
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IdentityResult> UpdateEmailAsync(string userId, string newEmail);
        Task<IdentityResult> DeleteAccountAsync(string userId);
    }
}
