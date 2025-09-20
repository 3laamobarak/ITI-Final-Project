using AutoMapper;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.Product;

namespace Company.Project.Application.Mapping.ProductMap
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductListDto>()
           .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name));

            CreateMap<Product, ProductSearchDto>().ReverseMap();
            CreateMap<Product, ProductDetailDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            

        }
    }
}
