using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Refund;

namespace Company.Project.Application.Services
{
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RefundService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Refund>> GetAllAsync(int skip, int take)
        {
            var refunds = _unitOfWork.RefundRepository.GetAllAsync(skip, take);
            return refunds;
        }

        public Task<Refund> GetByIdAsync(int id)
        {
            var refund = _unitOfWork.RefundRepository.GetByIdAsync(id);
            if (refund == null)
            {
                throw new KeyNotFoundException($"Refund with ID {id} not found.");
            }
            return refund;
        }

        public async Task<Refund> CreateAsync(CreateRefundDto createDto)
        {
            var refund = new Refund
            {
                Reason = createDto.Reason,
                Amount = createDto.Amount,
                RequestDate = DateTime.UtcNow,
                IsProcessed = false,
                OrderId = createDto.OrderId
            };
            await _unitOfWork.RefundRepository.AddAsync(refund);
            await _unitOfWork.RefundRepository.SaveChangesAsync();
            await Task.FromResult(refund);
            return refund;
        }

        public async Task<Refund> UpdateAsync(UpdateRefundDto updateDto)
        {
            var refund = _unitOfWork.RefundRepository.GetByIdAsync(updateDto.Id).Result;
            if (refund == null)
            {
                throw new KeyNotFoundException($"Refund with ID {updateDto.Id} not found.");
            }
            refund.Reason = updateDto.Reason ?? refund.Reason;
            refund.Amount = updateDto.Amount;
            refund.IsProcessed = updateDto.IsProcessed;
            if (updateDto.IsProcessed == true && refund.ProcessedDate == null)
            {
                refund.ProcessedDate = DateTime.UtcNow;
            }
            await _unitOfWork.RefundRepository.UpdateAsync(refund);
            await _unitOfWork.RefundRepository.SaveChangesAsync();
            await Task.FromResult(refund);
            return refund;
        }

        public async Task DeleteAsync(int id)
        {
            var refund = _unitOfWork.RefundRepository.GetByIdAsync(id).Result;
            if (refund == null)
            {
                throw new KeyNotFoundException($"Refund with ID {id} not found.");
            }
            await _unitOfWork.RefundRepository.DeleteAsync(refund);
            await _unitOfWork.RefundRepository.SaveChangesAsync();
            await Task.CompletedTask;
            return;
        }
    }
}
