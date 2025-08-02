using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Application.Contracts
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetUserCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task UpdateQuantityAsync(string userId, int itemId, int quantity);
        Task RemoveFromCartAsync(string userId, int itemId);
    }
}
