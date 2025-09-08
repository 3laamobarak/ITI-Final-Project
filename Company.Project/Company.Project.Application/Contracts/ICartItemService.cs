using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO;
namespace Company.Project.Application.Contracts
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetUserCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task UpdateQuantityAsync(string userId, int cartItemId, int quantity);
        Task RemoveFromCartAsync(string userId, int cartItemId);
        Task ClearCartAsync(string userId);
    }
}
