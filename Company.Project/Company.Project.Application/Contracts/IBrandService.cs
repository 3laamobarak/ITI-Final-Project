using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.BrandDTO;
using Company.Project.DTO.DTO.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Company.Project.Application.Contracts
{
    public interface IBrandService
    {


        Task<IEnumerable<BrandDto>> GetAllAsync(int skip, int take);
        Task<BrandWithProductsDto> GetByIdAsync(int id);
        Task<IEnumerable<BrandDto>> SearchAsync(string query);
        //Task<Brand> CreateAsync(CreateBrandDto createDto);
        //Task<Brand> UpdateAsync(UpdateBrandDto updateDto);
        //Task DeleteAsync(int id);

    }
}
