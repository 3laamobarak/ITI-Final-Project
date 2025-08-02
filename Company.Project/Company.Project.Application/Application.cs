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
            services.AddScoped<IExampleClassRepository, ExampleClassRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();



            #endregion

            #region Authentication JWT

            #endregion

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            
        }
        
    }
}
