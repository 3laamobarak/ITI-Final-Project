using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number with up to 2 decimal places")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or more")]
        public int StockQuantity { get; set; }

        public string? Overview { get; set; }
        public string? SuggestedUse { get; set; }
        public string? Warnings { get; set; }
        public string? Disclaimer { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Main image URL is required")]
 
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

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a brand")]
        public int BrandId { get; set; }
    }
}
