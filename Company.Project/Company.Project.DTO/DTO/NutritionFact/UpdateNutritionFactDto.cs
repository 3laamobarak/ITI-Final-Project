namespace Company.Project.DTO.DTO.NutritionFact
{
    public class UpdateNutritionFactDto
    {
        public int Id { get; set; }
        public string? Nutrient { get; set; }   
        public string? Amount { get; set; }     
        public string? DailyValue { get; set; } 
        public int ProductId { get; set; }
    }
}
