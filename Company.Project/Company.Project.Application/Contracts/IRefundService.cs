using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Refund;

namespace Company.Project.Application.Contracts
{
    public interface IRefundService
    {
        Task<IEnumerable<RefundDto>> GetAllAsync();
        Task<Refund> GetByIdAsync(int id);
        Task<Refund> CreateAsync(CreateRefundDto createDto);
        Task<Refund> UpdateAsync(UpdateRefundDto updateDto);
        Task DeleteAsync(int id);
        
    }
}
