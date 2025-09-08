using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Enums;
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

        public async Task CancelOrderAsync(int orderId, string userId)
        {
            var order = _unitOfWork.OrderRepository.GetOrderByIdAsync(orderId, userId);
            if (order == null || order.Result.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Order cannot be cancelled.");
            }
            order.Result.Status = OrderStatus.Cancelled;
            await _unitOfWork.OrderRepository.UpdateAsync(order.Result);
            await _unitOfWork.Completeasync();
            return;
        }

        public async Task DeleteOrderAsync(int orderId, string userId)
        {
            var order = _unitOfWork.OrderRepository.GetOrderByIdAsync(orderId, userId);
            if (order == null || order.Result.Status != OrderStatus.Cancelled)
            {
                throw new InvalidOperationException("Only cancelled orders can be deleted.");
            }
            await _unitOfWork.OrderRepository.DeleteAsync(order.Result);
            await _unitOfWork.Completeasync();
            return;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            if (!Enum.TryParse<OrderStatus>(status, true, out var newStatus))
            {
                throw new ArgumentException("Invalid status value.");
            }
            if (order.Result.Status == OrderStatus.Cancelled || order.Result.Status == OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Cannot change status of a cancelled or delivered order.");
            }
            order.Result.Status = newStatus;
            await _unitOfWork.OrderRepository.UpdateAsync(order.Result);
            await _unitOfWork.Completeasync();
            return;

        }
    }

}
