namespace Company.Project.DTO.DTO.NutritionFact
{
    public class CreateNutritionFactDto
    {
        public string? Nutrient { get; set; }   
        public string? Amount { get; set; }     
        public string? DailyValue { get; set; } 
        public int ProductId { get; set; }
    }
}
