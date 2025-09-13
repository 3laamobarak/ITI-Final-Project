using System.ComponentModel.DataAnnotations;

public class ProductCreateVm
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Display(Name = "Brand")]
    [Required(ErrorMessage = "Please select a brand")]
    public int BrandId { get; set; }

    [Display(Name = "Image Url")]
    public string ImageUrl { get; set; }
}
