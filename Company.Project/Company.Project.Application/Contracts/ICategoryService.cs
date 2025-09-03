using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.DTO.DTO.Category;
using Company.Project.DTO.DTO.Product;

namespace Company.Project.Application.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListDto>> GetAllAsync();
        Task<CategoryDetailDto> GetByIdAsync(int id);
        Task<IEnumerable<CategorySearchDto>> SearchAsync(string query);
        Task<CategoryDetailDto> CreateAsync(CreateCategoryDto createDto);
        Task<CategoryDetailDto> UpdateAsync(UpdateCategoryDto updateDto);
        Task DeleteAsync(int id);
        
    }
}
