using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.OrderItem;

namespace Company.Project.Application.Contracts
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetAllAsync(int skip, int take);
        Task<OrderItem> GetByIdAsync(int id);
        Task<OrderItem> CreateAsync(CreateOrderItemDto createDto);
        Task<OrderItem> UpdateAsync(UpdateOrderItemDto updateDto);
        Task DeleteAsync(int id);
        
    }
}
