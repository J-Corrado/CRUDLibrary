using CRUDLibrary.Data.LIB_DB;
using CRUDLibrary.Domain.Interfaces;
using CRUDLibrary.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CRUDLibrary.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LIBDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<Data.Seed>();
            services.AddScoped<IAuthor, Services.Author>();
            services.AddScoped<IBook, Services.Book>();
            services.AddScoped<IBorrower, Services.Borrower>();
            services.AddScoped<IDAL, DAL>();
            services.AddScoped<IValidation, Validation>();
            return services;
        }
    }
}
