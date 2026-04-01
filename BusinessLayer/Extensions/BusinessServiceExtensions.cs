using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Extensions
{
    public static class BusinessServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {      
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
            services.AddScoped<IJwtService, JwtService>();

            // Converters
            services.AddScoped<LengthUnitConverter>();
            services.AddScoped<WeightUnitConverter>();
            services.AddScoped<VolumeUnitConverter>();
            services.AddScoped<TemperatureUnitConverter>();

            return services;
        }
    }
}
