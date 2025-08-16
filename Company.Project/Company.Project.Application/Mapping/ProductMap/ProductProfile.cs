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
            CreateMap<Product, ProductSearchDto>().ReverseMap();
            CreateMap<Product, ProductDetailDto>().ReverseMap();

        }
    }
}
