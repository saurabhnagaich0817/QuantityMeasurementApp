using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Data
{
    // Empty DbContext - Auth Service no longer stores users
    // This is kept for future token blacklisting if needed
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) 
            : base(options) { }
        
        // No DbSet - Auth Service doesn't store users anymore
        // All user data comes from User Service via HTTP
    }
}