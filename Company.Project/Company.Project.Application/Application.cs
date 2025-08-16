using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Company.Project.Application.Contracts;
using Company.Project.Application.Services;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.Infrastructure.Repositories;
using Company.Project.Infrastructure.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.Project.Application
{
    public static class Application
    {
        public static void Application_CS(this IServiceCollection services, IConfiguration Configuration)
        {

            #region Services
                
            services.AddScoped<IExampleClassService, ExampleClassService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICartItemRepository, CartItemRepository>();

            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IOrderSevice, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Register AutoMapper
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());


            #endregion

            #region Authentication JWT

            #endregion

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
        
    }
}
