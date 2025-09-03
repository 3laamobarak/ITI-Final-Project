
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
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        }
        public CreateCategoryDto ToCreateCategoryDto(Category category)
        {
            return new CreateCategoryDto
            {
                Name = category.Name,
                Description = category.Description
            };
        }
        public UpdateCategoryDto ToUpdateCategoryDto(Category category)
        {
            return new UpdateCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
