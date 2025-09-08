using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<Payment> GetByPaymentIntentIdAsync(string paymentIntentId)
        {
            return await _dbContext.Payments
                .FirstOrDefaultAsync(p => p.PaymentIntentId == paymentIntentId);
        }
    }
}
