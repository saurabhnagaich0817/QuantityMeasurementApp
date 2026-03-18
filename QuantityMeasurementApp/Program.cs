using QuantityMeasurementApp.Factories;
using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Menu;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using RepoLayer.Interfaces;
#nullable enable
using QuantityMeasurementApp.Factories;
using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Menu;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        private static IServiceProvider? _serviceProvider;
        private static IConfiguration? _configuration;

        private static void Main(string[] args)
        {
            try
            {
                SetupConfiguration();
                SetupDependencyInjection();
                
                ShowRepositoryInfo();
                
                var menu = _serviceProvider!.GetRequiredService<IQuantityMeasurementAppMenu>();
                menu.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (_serviceProvider is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        private static void SetupConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static void SetupDependencyInjection()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_configuration!);
            
            // Register converters
            services.AddSingleton<LengthUnitConverter>();
            services.AddSingleton<WeightUnitConverter>();
            services.AddSingleton<VolumeUnitConverter>();
            services.AddSingleton<TemperatureUnitConverter>();
            
            // Register repository
            bool useDatabase = bool.Parse(_configuration!["AppSettings:UseDatabase"] ?? "false");
            
            if (useDatabase)
            {
                string? connectionString = _configuration!.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    services.AddSingleton<IQuantityRepository>(new QuantityDatabaseRepository(connectionString));
                }
                else
                {
                    services.AddSingleton<IQuantityRepository, QuantityRepository>();
                }
            }
            else
            {
                services.AddSingleton<IQuantityRepository, QuantityRepository>();
            }
            
            // Register service with all dependencies
            services.AddSingleton<IQuantityMeasurementService, QuantityMeasurementService>();
            
            // Register menu
            services.AddSingleton<IMenuFactory, MenuFactory>();
            services.AddSingleton<IQuantityMeasurementAppMenu, QuantityMeasurementAppMenu>();
            
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ShowRepositoryInfo()
        {
            var repo = _serviceProvider!.GetRequiredService<IQuantityRepository>();
            
            if (repo is QuantityDatabaseRepository)
            {
                Console.WriteLine(" Using DATABASE repository");
            }
            else
            {
                Console.WriteLine(" Using CACHE repository");
            }
        }
    }
}