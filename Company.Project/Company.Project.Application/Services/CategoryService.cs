using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.DTO.DTO.Category;

namespace Company.Project.Application.Services
{
   public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService( IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // get all 
        public async Task<IEnumerable<CategoryListDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<CategoryListDto>();
            }

            return _mapper.Map<IEnumerable<CategoryListDto>>(categories);
        }

        // get by id
        public async Task<CategoryDetailDto> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return _mapper.Map<CategoryDetailDto>(category);
        }

        // search category by query
        public async Task<IEnumerable<CategorySearchDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<CategorySearchDto>();
            }
            var categories = await _unitOfWork.CategoryRepository.SearchAsync(query);

            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<CategorySearchDto>();
            }

            return _mapper.Map<IEnumerable<CategorySearchDto>>(categories);
        }

    }
}
