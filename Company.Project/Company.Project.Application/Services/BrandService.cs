using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.BrandDTO;
using Company.Project.DTO.DTO.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync(int skip, int take)
        {
            var brands = await _unitOfWork.BrandRepository.GetAllAsync(skip, take);
            return brands.Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description
                
            });
        }

        public async Task<BrandWithProductsDto> GetByIdAsync(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if (brand == null) return null;

            return new BrandWithProductsDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Products = brand.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).ToList()
            };
        }

        public async Task<IEnumerable<BrandDto>> SearchAsync(string query)
        {
            var brands = await _unitOfWork.BrandRepository.GetByExpressionAsync(b => b.Name.Contains(query));
            return brands.Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Description= b.Description
            });
        }

        //public async Task<Brand> CreateAsync(CreateBrandDto createDto)
        //{
        //    var brand = new Brand { Name = createDto.Name };
        //    await _unitOfWork.BrandRepository.AddAsync(brand);
        //    await _unitOfWork.Completeasync();
        //    return brand;
        //}

        //public async Task<Brand> UpdateAsync(UpdateBrandDto updateDto)
        //{
        //    var brand = await _unitOfWork.BrandRepository.GetByIdAsync(updateDto.Id);
        //    if (brand == null) throw new KeyNotFoundException();
        //    brand.Name = updateDto.Name;
        //    await _unitOfWork.BrandRepository.UpdateAsync(brand);
        //    await _unitOfWork.Completeasync();
        //    return brand;
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
        //    if (brand == null) throw new KeyNotFoundException();
        //    await _unitOfWork.BrandRepository.DeleteAsync(brand);
        //    await _unitOfWork.Completeasync();
        //}

    }
}
