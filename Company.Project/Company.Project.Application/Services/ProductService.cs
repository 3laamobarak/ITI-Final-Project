using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Product;

namespace Company.Project.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        // autompper 
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // get all products
        public async Task<IEnumerable<ProductListDto>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductListDto>();
            }

            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }


        // get product by id
        public async Task<ProductDetailDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            }

            return _mapper.Map<ProductDetailDto>(product);
          
        }

        // search products by query
        public async Task<IEnumerable<ProductSearchDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Search query cannot be null or empty.", nameof(query));
            }

            var products = await _unitOfWork.ProductRepository.SearchAsync(query);

            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductSearchDto>();
            }

            return _mapper.Map<IEnumerable<ProductSearchDto>>(products);
        }

        public async Task<ProductDetailDto> CreateAsync(CreateProductDto createDto)
        {
            var product = _mapper.Map<Product>(createDto);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.Completeasync();
            return await Task.FromResult(_mapper.Map<ProductDetailDto>(product));
        }
        public async Task<ProductDetailDto> UpdateAsync(UpdateProductDto updateDto)
        {
            var existingProduct = _unitOfWork.ProductRepository.GetByIdAsync(updateDto.Id).Result;
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {updateDto.Id} not found.");
            }
            _mapper.Map(updateDto, existingProduct);
            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await _unitOfWork.Completeasync();
            return await Task.FromResult(_mapper.Map<ProductDetailDto>(existingProduct));
        }
        public async Task DeleteAsync(int id)
        {
            var existingProduct = _unitOfWork.ProductRepository.GetByIdAsync(id).Result;
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            await _unitOfWork.ProductRepository.DeleteAsync(existingProduct);
            await _unitOfWork.Completeasync();
            await Task.CompletedTask;
            return;
        }
    }
}
