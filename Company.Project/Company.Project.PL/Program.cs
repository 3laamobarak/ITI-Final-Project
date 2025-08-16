using Company.Project.Application;
using Company.Project.Application.Contracts;
using Company.Project.Application.Services;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.ExampleClass;
using Company.Project.Infrastructure;
using Company.Project.Infrastructure.Repositories;
using Company.Project.Infrastructure.UnitOfWork;
using Company.Project.theDbcontext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
namespace Company.Project.PL

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
            // call the infrastructure and application methods
            builder.Services.Application_CS(builder.Configuration);
            builder.Services.Infrastructure_CS(builder.Configuration);

            builder.Services.AddSwaggerGen(c =>
            {
                // other options ...
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });


            // validation
            builder.Services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateExampleClassDto>()
                      .RegisterValidatorsFromAssemblyContaining<UpdateExampleClassDto>();
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            builder.Services.AddScoped<IOrderSevice, OrderService>();
            builder.Services.AddScoped<IorderRepository, OrderRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();



            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();

       
        }
    }
}
