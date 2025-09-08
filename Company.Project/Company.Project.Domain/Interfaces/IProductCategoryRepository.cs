using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync(int skip, int take);
    }
}
