using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Category;
using Company.Project.DTO.DTO.Product;

namespace Company.Project.Application.Contracts
{
    public interface ICategoryService
    {
        // get all 
        // get all 
        // get all 
        Task<IEnumerable<CategoryListDto>> GetAllAsync();

        // Get all products for a specific category
        Task<IEnumerable<Product>> GetallCateogryProducts(int categoryId);
        Task<CategoryDetailDto> CreateAsync(CreateCategoryDto createDto);
        Task<CategoryDetailDto> UpdateAsync(UpdateCategoryDto updateDto);
        Task DeleteAsync(int id);
        
        Task<IEnumerable<Product>> GetallCateogryProducts(int categoryId);

    }
}
