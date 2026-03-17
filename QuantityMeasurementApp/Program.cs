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
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        private static void Main(string[] args)
        {
            try
            {
                SetupConfiguration();
                SetupDependencyInjection();
                
                ShowRepositoryInfo();
                TestDatabaseConnection();
                
                var menu = _serviceProvider.GetRequiredService<IQuantityMeasurementAppMenu>();
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
            services.AddSingleton(_configuration);
            
            bool useDatabase = bool.Parse(_configuration["AppSettings:UseDatabase"] ?? "false");
            
            if (useDatabase)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                services.AddSingleton<IQuantityRepository>(new QuantityDatabaseRepository(connectionString));
            }
            else
            {
                services.AddSingleton<IQuantityRepository, QuantityRepository>();
            }
            
            services.AddSingleton<IQuantityMeasurementService, QuantityMeasurementService>();
            services.AddSingleton<IMenuFactory, MenuFactory>();
            services.AddSingleton<IQuantityMeasurementAppMenu, QuantityMeasurementAppMenu>();
            
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ShowRepositoryInfo()
        {
            var repo = _serviceProvider.GetRequiredService<IQuantityRepository>();
            
            if (repo is QuantityDatabaseRepository)
            {
                Console.WriteLine(" Using ADVANCED DATABASE repository (UC16)");
            }
            else
            {
                Console.WriteLine(" Using CACHE repository");
            }
        }

        private static void TestDatabaseConnection()
        {
            var repo = _serviceProvider.GetRequiredService<IQuantityRepository>();
            
            if (repo is QuantityDatabaseRepository dbRepo)
            {
                try
                {
                    int count = dbRepo.GetTotalCount();
                    Console.WriteLine($"Database connected. Total records: {count}");
                    
                    // Show quick stats
                    if (count > 0)
                    {
                        var latest = dbRepo.GetAllFromDatabase();
                        if (latest.Count > 0)
                        {
                            Console.WriteLine($"Latest: {latest[0].OperationType} at {latest[0].CreatedAt:HH:mm}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Database warning: {ex.Message}");
                    
                    // Fall back to cache
                    var services = new ServiceCollection();
                    services.AddSingleton(_configuration);
                    services.AddSingleton<IQuantityRepository, QuantityRepository>();
                    services.AddSingleton<IQuantityMeasurementService, QuantityMeasurementService>();
                    services.AddSingleton<IMenuFactory, MenuFactory>();
                    services.AddSingleton<IQuantityMeasurementAppMenu, QuantityMeasurementAppMenu>();
                    
                    _serviceProvider = services.BuildServiceProvider();
                }
            }
        }
    }
}