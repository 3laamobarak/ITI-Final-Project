using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
