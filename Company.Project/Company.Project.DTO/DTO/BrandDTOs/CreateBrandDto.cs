using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.BrandDTO
{
   
        public class CreateBrandDto
        {
            [Required(ErrorMessage = "Brand name is required.")]
            [StringLength(100, MinimumLength = 3,
                ErrorMessage = "Brand name must be between 3 and 100 characters.")]
            public string Name { get; set; }

            [StringLength(500, MinimumLength = 10 ,  ErrorMessage = "Description must be between 10 and 500 characters.")]
            public string Description { get; set; }
        }
    
}
