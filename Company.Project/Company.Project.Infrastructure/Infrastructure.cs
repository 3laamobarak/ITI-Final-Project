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
            
            services.AddScoped<IExampleClassRepository, ExampleClassRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IorderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<INutritionFactRepository, NutritionFactRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRefundRepository, RefundRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            #endregion
        }
    }
}
