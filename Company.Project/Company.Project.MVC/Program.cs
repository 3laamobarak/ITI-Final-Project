using AutoMapper;
using Company.Project.Application.Mapping.ProductMap;
using Company.Project.Application.Services;
using Company.Project.Domain.Interfaces;
using Company.Project.Infrastructure.UnitOfWork;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

           


            // Register in DI

            // Register UoW + Services
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<ProductService>();
            //builder.Services.AddScoped<CategoryService>();
            //builder.Services.AddScoped<BrandService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
