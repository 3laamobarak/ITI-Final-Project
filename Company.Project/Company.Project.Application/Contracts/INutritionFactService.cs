using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.NutritionFact;

namespace Company.Project.Application.Contracts
{
    public interface INutritionFactService
    {
        Task<IEnumerable<NutritionFact>> GetAllAsync(int skip, int take);
        Task<NutritionFact> GetByIdAsync(int id);
        Task<NutritionFact> CreateAsync(CreateNutritionFactDto createDto);
        Task<NutritionFact> UpdateAsync(UpdateNutritionFactDto updateDto);
        Task DeleteAsync(int id);
    }
}
