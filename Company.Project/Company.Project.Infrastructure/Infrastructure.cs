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


            services.AddScoped<IExampleClassRepository, ExampleClassRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IorderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            #endregion
        }
    }
}
