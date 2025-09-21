using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Company.Project.DTO.DTO.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string? Overview { get; set; }
        public string? SuggestedUse { get; set; }
        public string? Warnings { get; set; }
        public string? Disclaimer { get; set; }
        public DateTime ExpiryDate { get; set; }
        // public string ImageUrl { get; set; }
        // public string? Imageone { get; set; }
        //
        // public string? Imagetwo { get; set; }
        //
        // public string? Imagethree { get; set; }
        //
        // public string? Imagefour { get; set; }
        //
        // public string? Imagefive { get; set; }
        public IFormFile? image { get; set; }
        public string? Imagepath { get; set; }

        public IFormFile? image2 { get; set; }
        public string? Imagepath2 { get; set; }

        public IFormFile? image3 { get; set; }
        public string? Imagepath3 { get; set; }
        
        public IFormFile? image4 { get; set; }
        public string? Imagepath4 { get; set; }
        
        public IFormFile? image5 { get; set; }
        public string? Imagepath5 { get; set; }



        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public CategoryDto Category { get; set; }
        public BrandDto Brand { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

