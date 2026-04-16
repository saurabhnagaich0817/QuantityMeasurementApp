using System;

namespace AuthService.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email, string username);
        bool ValidateToken(string token);
    }
}