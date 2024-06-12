using Api.Services.Interficies;
using Api.Services;
using Azure.Identity;
using Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Data.Repository.Interfaces;
using Data.Repository;
using Data.Repositories;
using Data.Interficies;
using Data.Database.Entities;
using Data.Database.Validations;
using System.Reflection;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped<IValidation<Product>, ProductValidation>();
            services.AddScoped<IValidation<Color>, ColorValidation>();
            services.AddScoped<IValidation<Size>, SizeValidation>();

            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
