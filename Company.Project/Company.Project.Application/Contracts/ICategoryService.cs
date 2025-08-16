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

        // get all 
        Task<IEnumerable<CategoryListDto>> GetAllAsync();

        // get by id
        Task<CategoryDetailDto> GetByIdAsync(int id);

        // search
        Task<IEnumerable<CategorySearchDto>> SearchAsync(string query);


    }
}
