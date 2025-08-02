using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;

namespace Company.Project.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartItem>> GetUserCartAsync(string userId)
        {
            return await _unitOfWork.CartItemRepository.GetByExpressionAsync(c => c.userId == userId, includeProperties: "product");
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cartItem = new CartItem
            {
                userId = userId,
                productId = productId,
                quantity = quantity
            };

            await _unitOfWork.CartItemRepository.AddAsync(cartItem);
            await _unitOfWork.Completeasync();
        }
        public async Task UpdateQuantityAsync(string userId, int itemId, int quantity)
        {
            var item = await _unitOfWork.CartItemRepository
                .FirstOrDefaultAsync(c => c.Id == itemId && c.userId == userId);

            if (item == null)
                throw new Exception("Cart item not found");

            item.quantity = quantity;
            await _unitOfWork.CartItemRepository.UpdateAsync(item);
            await _unitOfWork.Completeasync();
        }

        public async Task RemoveFromCartAsync(string userId, int itemId)
        {
            var item = await _unitOfWork.CartItemRepository
                .FirstOrDefaultAsync(c => c.Id == itemId && c.userId == userId);

            if (item == null)
                throw new Exception("Cart item not found");

            await _unitOfWork.CartItemRepository.HardDeleteAsync(item);
            await _unitOfWork.Completeasync();
        }
    }
}
