using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Company.Project.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserWithDetailsAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);
    }
}
