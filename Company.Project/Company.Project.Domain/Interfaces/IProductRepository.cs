using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> SearchAsync(string query);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
