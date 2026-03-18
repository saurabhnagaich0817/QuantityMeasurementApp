using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;  
using QuantityMeasurementApp.API.Middleware;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Quantity Measurement API",
        Version = "v1",
        Description = "API for quantity measurement operations (Length, Weight, Volume, Temperature)",
        Contact = new OpenApiContact
        {
            Name = "UC17 Implementation",
            Email = "your-email@example.com"
        }
    });
});

// Register dependencies
builder.Services.AddScoped<LengthUnitConverter>();
builder.Services.AddScoped<WeightUnitConverter>();
builder.Services.AddScoped<VolumeUnitConverter>();
builder.Services.AddScoped<TemperatureUnitConverter>();

// Repository - choose based on configuration
var useDatabase = builder.Configuration.GetValue<bool>("AppSettings:UseDatabase");
if (useDatabase)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddSingleton<IQuantityRepository>(sp => 
        new QuantityDatabaseRepository(connectionString!));
}
else
{
    builder.Services.AddSingleton<IQuantityRepository, QuantityRepository>();
}

builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

// Global exception handling middleware
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

// Database check on startup
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IQuantityRepository>();
    if (repo is QuantityDatabaseRepository dbRepo)
    {
        try
        {
            var count = dbRepo.GetTotalCount();
            Console.WriteLine($"Database connected. Total records: {count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Database warning: {ex.Message}");
        }
    }
}

app.Run();