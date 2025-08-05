using Company.Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Domain.Interfaces
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Task<IEnumerable<Brand>> GetAllAsync(int skip, int take);
        Task<IEnumerable<Brand>> SearchAsync(string query);
        Task<Brand?> GetBrandWithProductsAsync(int id);
    }
}
