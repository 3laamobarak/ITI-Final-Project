using Company.Project.Application.Contracts;
using Company.Project.Application.Services;
using Company.Project.Domain.Interfaces;
using Company.Project.Infrastructure.UnitOfWork;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;
using Company.Project.Application;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.OTPs;
using Company.Project.Infrastructure;
using Company.Project.theDbcontext;
using Microsoft.AspNetCore.Identity;
using Company.Project.Application.Contracts;
using Company.Project.Application.Services;
using Company.Project.Domain.Interfaces;
using Company.Project.Infrastructure.Repositories;
using Company.Project.Infrastructure.UnitOfWork;
using Company.Project.theDbcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
    

namespace Company.Project.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add DbContext
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Add UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<BrandService>();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<Company.Project.Application.Mapping.CategoryMap.CategoryProfile>();
            });



            // Add controllers
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // allow CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            // bind email 
            builder.Services.Configure<EmailSettingsDTO>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            // call the infrastructure and application methods
            builder.Services.Application_CS(builder.Configuration);
            builder.Services.Infrastructure_CS(builder.Configuration);

            builder.Services.AddScoped<IPaymentService, StripePaymentService>();
            builder.Services.AddScoped<IRefundService, RefundService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy => policy.RequireRole("admin"));
            });

            var app = builder.Build();

        

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
