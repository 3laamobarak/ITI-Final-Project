using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface INutritionFactRepository : IBaseRepository<NutritionFact>
    {
        Task<IEnumerable<NutritionFact>> GetAllAsync(int skip, int take);
    }
}
