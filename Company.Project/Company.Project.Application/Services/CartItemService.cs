using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO;

namespace Company.Project.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItemDto>> GetUserCartAsync(string userId)
        {
            var items = await _unitOfWork.CartItemRepository.GetUserCartAsync(userId);
            return items.Select(c => new CartItemDto
            {
                CartItemId = c.Id,
                ProductId = c.productId,
                ProductName = c.product.Name,
                Quantity = c.quantity,
                Price = c.product.Price,
                Description = c.product.Description ?? "بدون وصف"
            }).ToList();
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("الكمية يجب أن تكون أكبر من صفر");
            var product = await _unitOfWork.ProductRepository.GetByExpressionSingleAsync(p => p.Id == productId);
            if (product == null)
                throw new Exception("المنتج غير موجود");
            var existingItem = await _unitOfWork.CartItemRepository.GetUserCartItemAsync(userId, productId);
            if (existingItem != null)
            {
                existingItem.quantity += quantity;
                await _unitOfWork.CartItemRepository.UpdateAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    userId = userId,
                    productId = productId,
                    quantity = quantity
                };
                await _unitOfWork.CartItemRepository.AddAsync(cartItem);
            }
            await _unitOfWork.Completeasync();
        }

        public async Task UpdateQuantityAsync(string userId, int itemId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("الكمية يجب أن تكون أكبر من صفر");
            var item = await _unitOfWork.CartItemRepository.GetByExpressionSingleAsync(c => c.Id == itemId && c.userId == userId);
            if (item == null)
                throw new Exception("العنصر غير موجود في العربية");
            item.quantity = quantity;
            await _unitOfWork.CartItemRepository.UpdateAsync(item);
            await _unitOfWork.Completeasync();
        }

        public async Task RemoveFromCartAsync(string userId, int itemId)
        {
            var item = await _unitOfWork.CartItemRepository.GetByExpressionSingleAsync(c => c.Id == itemId && c.userId == userId);
            if (item == null)
                throw new Exception("العنصر غير موجود في العربية");
            await _unitOfWork.CartItemRepository.HardDeleteAsync(item);
            await _unitOfWork.Completeasync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var items = await _unitOfWork.CartItemRepository.GetUserCartAsync(userId);
            if (items.Any())
            {
                await _unitOfWork.CartItemRepository.DeleteRangeAsync(items.ToList());
            }
            await _unitOfWork.Completeasync();
        }
    }
}