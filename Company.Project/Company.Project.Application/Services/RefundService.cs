using System.Linq.Expressions;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Refund;
using static Company.Project.Domain.Enums.Enums;

namespace Company.Project.Application.Services
{
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RefundService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<RefundDto>> GetAllAsync()
        {
            var refunds = await _unitOfWork.RefundRepository.GetAllAsync(new Expression<Func<Refund, object>>[]
            {
        r => r.Payment,
        r => r.Payment.User 
            });

            return refunds.Select(r => new RefundDto
            {
                Id = r.Id,
                Amount = r.Amount,
                Reason = r.Reason,
                RequestDate = r.RequestDate,
                Status = (int)r.Status,
                IsDeleted = r.IsDeleted,
                PaymentId = (int)r.PaymentId,
                UserName = r.Payment.User?.UserName,
                FullName = r.Payment.User != null ? $"{r.Payment.User.FirstName} {r.Payment.User.LastName}" : null
            }).ToList();
        }


        public async Task<Refund> GetByIdAsync(int id)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(id);
            if (refund == null)
                throw new KeyNotFoundException($"Refund with ID {id} not found.");

            return refund;
        }

        public async Task<Refund> CreateAsync(CreateRefundDto createDto)
        {
            var refund = new Refund
            {
                Reason = createDto.Reason,
                Amount = createDto.Amount,
                RequestDate = DateTime.UtcNow,
                Status = RefundStatus.Pending,
                OrderId = createDto.OrderId,
                PaymentId = createDto.PaymentId
            };
            await _unitOfWork.RefundRepository.AddAsync(refund);
            await _unitOfWork.RefundRepository.SaveChangesAsync();
            await Task.FromResult(refund);
            return refund;
        }
        public async Task<Refund> UpdateAsync(UpdateRefundDto updateDto)
        {
            var refund = await _unitOfWork.RefundRepository.GetByIdAsync(updateDto.Id);
            if (refund == null)
                throw new KeyNotFoundException($"Refund with ID {updateDto.Id} not found.");

            refund.Status = updateDto.Status;
            refund.ProcessedDate = updateDto.ProcessedDate ?? DateTime.UtcNow;

            await _unitOfWork.RefundRepository.UpdateAsync(refund);
            await _unitOfWork.RefundRepository.SaveChangesAsync();

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
