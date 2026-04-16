using System;
using System.Threading.Tasks;

namespace AuthService.Core.Interfaces
{
    public interface IUserServiceClient
    {
        Task<UserRegistrationResult> RegisterUserAsync(string username, string email, string password);
        Task<UserValidationResult> ValidateUserAsync(string email, string password);
    }

    public class UserRegistrationResult
    {
        public bool Success { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class UserValidationResult
    {
        public bool IsValid { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? ErrorMessage { get; set; }
    }
}