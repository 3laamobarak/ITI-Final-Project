using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IRefundRepository : IBaseRepository<Refund>
    {
        Task<IEnumerable<Refund>> GetAllAsync(int skip, int take);
    }
}
