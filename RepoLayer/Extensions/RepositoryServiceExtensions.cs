using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoLayer.Auth;
using RepoLayer.Context;
using RepoLayer.Interfaces;
using RepoLayer.Persistence;

namespace RepoLayer.Extensions
{
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuantityRepository, QuantityRepository>();

            return services;
        }
    }
}
