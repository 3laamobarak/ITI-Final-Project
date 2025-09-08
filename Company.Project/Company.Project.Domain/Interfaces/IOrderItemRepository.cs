using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetAllAsync(int skip, int take);
    }
}
