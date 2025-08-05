using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Order;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Application.Services
{
    public class OrderService : IOrderSevice
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> PlaceOrderAsync(CreateOrderDto dto, string userId)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                ShippingAddress = dto.ShippingAddress,
                Subtotal = dto.Subtotal,
                Tax = dto.Tax,
                Discount = dto.Discount,
                ShippingCost = dto.ShippingCost,
                UserId = userId,
                OrderType = OrderType.
                Online
            };

            foreach (var item in dto.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.Completeasync();
            return order.Id;
        }

        public async Task<List<OrderDetailsDto>> GetOrdersForUserAsync(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);

            return orders.Select(o => new OrderDetailsDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status.ToString(),
                Total = o.Total,
                ShippingAddress = o.ShippingAddress,
                OrderItems = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDetailsDto?> GetOrderByIdAsync(int id, string userId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id, userId);
            if (order == null) return null;

            return new OrderDetailsDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                Total = order.Total,
                ShippingAddress = order.ShippingAddress,
                OrderItems = order.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }

}
