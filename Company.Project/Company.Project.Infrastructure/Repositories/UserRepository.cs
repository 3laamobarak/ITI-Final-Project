using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly Context _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(Context context, UserManager<ApplicationUser> userManager)
            : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserWithDetailsAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                //.Include(u => u.Chats)
                .Include(u => u.ChatMessages)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
