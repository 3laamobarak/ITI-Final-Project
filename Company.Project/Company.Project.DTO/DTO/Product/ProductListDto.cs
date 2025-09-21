using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Company.Project.DTO.DTO.Product
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        // add imageUrl property
        // public string ImageUrl { get; set; }

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


    }
}
