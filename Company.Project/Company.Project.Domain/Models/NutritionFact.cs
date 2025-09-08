using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Domain.Models
{
  public class NutritionFact : BaseEntity
{
    public string? Nutrient { get; set; }    // Vitamin C"
    public string? Amount { get; set; }     // "90 mg"
    public string? DailyValue { get; set; }          // "100%"

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }

}
