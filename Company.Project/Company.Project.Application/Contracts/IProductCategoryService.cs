using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.ProductCategory;

namespace Company.Project.Application.Contracts
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync(int skip, int take);
        Task<ProductCategory> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(CreateProductCategoryDto createDto);
        Task<ProductCategory> UpdateAsync(UpdateProductCategoryDto updateDto);
        Task DeleteAsync(int id);
        
    }
}
