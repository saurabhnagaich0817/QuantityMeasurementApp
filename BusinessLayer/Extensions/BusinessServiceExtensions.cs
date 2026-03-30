using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Extensions
{
    public static class BusinessServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
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
