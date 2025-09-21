using System.ComponentModel.DataAnnotations;

namespace Company.Project.DTO.DTO.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Name can only contain letters, numbers and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "Description must be between 5 and 500 characters")]
        public string Description { get; set; }
    }
}
