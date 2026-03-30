using ModelLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
