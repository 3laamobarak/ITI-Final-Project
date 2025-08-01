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
    }

}
