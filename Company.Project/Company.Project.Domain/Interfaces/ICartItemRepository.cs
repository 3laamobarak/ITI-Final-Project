using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetUserCartAsync(string userId);
        Task<CartItem?> GetUserCartItemAsync(string userId, int productId);
    }
}
