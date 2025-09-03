namespace Company.Project.DTO.DTO.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string? Overview { get; set; }  
        public string? SuggestedUse { get; set; }
        public string? Warnings { get; set; } 
        public string? Disclaimer { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public string ImageUrl { get; set; }
        public string? image2Url { get; set; }
        public string? image3Url { get; set; }
        public string? image4Url { get; set; }
        public string? image5Url { get; set; }
        
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
