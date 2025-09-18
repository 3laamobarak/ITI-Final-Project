using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
      Task<IEnumerable<Review>>GetProductreviewsbyidAsync(int  productid);
      Task<Review?> GetUserReviewForProductAsync(int productId, string userId);
      Task<IEnumerable<Review>> GetUserReviewsAsync(string userId);
    }
}
