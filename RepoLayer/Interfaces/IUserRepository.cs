#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace RepoLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task UpdateAsync(User user);
    }
}
