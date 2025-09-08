using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.theDbcontext;

namespace Company.Project.Infrastructure.Repositories
{
    public class NutritionFactRepository : BaseRepository<NutritionFact>, INutritionFactRepository
    {
        public NutritionFactRepository(Context context) : base(context)
        {

        }
    }
}
