using Company.Project.Domain.Interfaces;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Project.Infrastructure
{
    public static class Infrastructure
    {
        public static void Infrastructure_CS(this IServiceCollection services, IConfiguration Configuration)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
 
            
            #region Database Context

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #endregion
        }
        
    }
}
