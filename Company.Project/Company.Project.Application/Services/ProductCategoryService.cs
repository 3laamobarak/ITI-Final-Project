using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;

namespace Company.Project.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork; 
        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductCategory>> GetAllAsync(int skip, int take)
        {
            return await _unitOfWork.ProductCategoryRepository.GetAllAsync(skip, take);
        }
        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);
        }
        public async Task<ProductCategory> CreateAsync(DTO.DTO.ProductCategory.CreateProductCategoryDto createDto)
        {
            var productCategory = new ProductCategory
            {
                ProductId = createDto.ProductId,
                CategoryId = createDto.CategoryId
            };
            await _unitOfWork.ProductCategoryRepository.AddAsync(productCategory);
            await _unitOfWork.Completeasync();
            return productCategory;
        }
        public async Task<ProductCategory> UpdateAsync(DTO.DTO.ProductCategory.UpdateProductCategoryDto updateDto)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(updateDto.ProductId);
            if (productCategory == null)
            {
                throw new Exception("ProductCategory not found");
            }
            productCategory.ProductId = updateDto.ProductId;
            productCategory.CategoryId = updateDto.CategoryId;
            await _unitOfWork.ProductCategoryRepository.UpdateAsync(productCategory);
            await _unitOfWork.Completeasync();
            return productCategory;
        }
        public async Task DeleteAsync(int id)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                throw new Exception("ProductCategory not found");
            }
            await _unitOfWork.ProductCategoryRepository.DeleteAsync(productCategory);
            await _unitOfWork.Completeasync();
        }
    }
}
