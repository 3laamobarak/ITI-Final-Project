using Company.Project.Domain.Interfaces;
using Company.Project.Infrastructure.Repositories;
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
            #region Database Context

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            #endregion
        }
    }
}
