using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AuthService.Core.Interfaces;

namespace AuthService.Infrastructure.Services
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _userServiceBaseUrl;

        public UserServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _userServiceBaseUrl = configuration["UserService:BaseUrl"] ?? "http://localhost:5002";
        }

        public async Task<UserRegistrationResult> RegisterUserAsync(string username, string email, string password)
        {
            try
            {
                // Create request object
                var requestObj = new
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                
                var jsonRequest = JsonSerializer.Serialize(requestObj);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_userServiceBaseUrl}/api/User/register", content);
                var responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<UserServiceRegisterResponse>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    if (result != null && result.Success && result.Data != null)
                    {
                        return new UserRegistrationResult
                        {
                            Success = true,
                            UserId = result.Data.Id,
                            Username = result.Data.Username,
                            Email = result.Data.Email
                        };
                    }
                }
                
                return new UserRegistrationResult
                {
                    Success = false,
                    ErrorMessage = $"User service error: {response.StatusCode} - {responseBody}"
                };
            }
            catch (Exception ex)
            {
                return new UserRegistrationResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<UserValidationResult> ValidateUserAsync(string email, string password)
        {
            try
            {
                var requestObj = new { Email = email, Password = password };
                var jsonRequest = JsonSerializer.Serialize(requestObj);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_userServiceBaseUrl}/api/User/validate", content);
                var responseBody = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<UserValidateResponse>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    if (result != null)
                    {
                        return new UserValidationResult
                        {
                            IsValid = result.IsValid,
                            UserId = result.UserId,
                            Username = result.Username,
                            Email = result.Email
                        };
                    }
                }
                
                return new UserValidationResult
                {
                    IsValid = false,
                    ErrorMessage = $"Validation failed: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                return new UserValidationResult
                {
                    IsValid = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        // Response DTOs
        private class UserServiceRegisterResponse
        {
            public bool Success { get; set; }
            public UserData Data { get; set; } = new();
        }

        private class UserData
        {
            public Guid Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        private class UserValidateResponse
        {
            public bool IsValid { get; set; }
            public Guid? UserId { get; set; }
            public string? Username { get; set; }
            public string? Email { get; set; }
        }
    }
}