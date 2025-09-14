using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.DTO.DTO.BrandDTOs
{
    public class BrandWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductDto> Products { get; set; } = new();

    }
}
