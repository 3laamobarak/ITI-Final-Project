using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Order;

namespace Company.Project.Application.Contracts
{
    public interface IOrderSevice
    {
        Task<int> PlaceOrderAsync(CreateOrderDto dto, string userId);
        Task<List<OrderDetailsDto>> GetOrdersForUserAsync(string userId);
        Task<OrderDetailsDto?> GetOrderByIdAsync(int id, string userId);
        Task CancelOrderAsync(int orderId, string userId);
        Task DeleteOrderAsync(int orderId, string userId);
        Task UpdateOrderStatusAsync(int orderId, string status);
        
    }
 }
