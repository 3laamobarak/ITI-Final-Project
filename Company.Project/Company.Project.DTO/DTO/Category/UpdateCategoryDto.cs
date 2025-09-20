using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Category;

public class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 3,
          ErrorMessage = "Name must be between 3 and 100 characters.")]
    public string Name { get; set; }

    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
    public string Description { get; set; }
}