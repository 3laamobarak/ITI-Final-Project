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
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly Context _context;
        public ReviewRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetProductreviewsbyidAsync(int productid)
        {
            return await _context.reviews
                       .Where(r => r.ProductId == productid).Include(r=>r.User).ToListAsync();
        }

    }
}
