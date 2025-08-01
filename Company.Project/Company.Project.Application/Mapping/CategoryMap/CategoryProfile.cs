
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Category;
using AutoMapper;

namespace Company.Project.Application.Mapping.CategoryMap
{
  public  class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>()
                .ReverseMap();
            CreateMap<Category,CategoryDetailDto>()
                .ReverseMap();
            CreateMap<Category, CategorySearchDto>()
                .ReverseMap();
        }
    }
}
