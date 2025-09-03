using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Product;

namespace Company.Project.Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetAllAsync();
        Task<ProductDetailDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductSearchDto>> SearchAsync(string query);
        Task<ProductDetailDto> CreateAsync(CreateProductDto createDto);
        Task<ProductDetailDto> UpdateAsync(UpdateProductDto updateDto);
        Task DeleteAsync(int id);
    }
}