using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;

namespace Company.Project.Infrastructure.Repositories
{
    public class RefundRepository : BaseRepository<Refund>, IRefundRepository
    {
        public RefundRepository(Context context) : base(context)
        {
            
        }
        
    }
}
