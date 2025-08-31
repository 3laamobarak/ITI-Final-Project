using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.Infrastructure.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<CartItem>> GetUserCartAsync(string userId)
        {
            return await _dbContext.CartItems
                .Where(c => c.userId == userId)
                .Include(c => c.product)
                .ToListAsync();
        }

        public async Task<CartItem?> GetUserCartItemAsync(string userId, int productId)
        {
            return await _dbContext.CartItems
                .Include(c => c.product)
                .FirstOrDefaultAsync(c => c.userId == userId && c.productId == productId);
        }
    }
}