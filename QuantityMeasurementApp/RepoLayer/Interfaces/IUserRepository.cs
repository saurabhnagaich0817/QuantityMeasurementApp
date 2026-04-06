#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace RepoLayer.Interfaces
{
    /// <summary>
    /// Repository interface for user account operations.
    /// Handles user creation, retrieval, and validation.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>Retrieves a user by their unique identifier asynchronously.</summary>
        /// <param name="id">The user's unique ID.</param>
        /// <returns>The user if found; otherwise null.</returns>
        Task<User?> GetByIdAsync(int id);

        /// <summary>Retrieves a user by their email address asynchronously.</summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The user if found; otherwise null.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>Retrieves a user by their username asynchronously.</summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The user if found; otherwise null.</returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>Creates a new user account asynchronously.</summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>The created user with generated ID.</returns>
        Task<User> CreateAsync(User user);

        /// <summary>Checks if an email address is already registered asynchronously.</summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if email exists; otherwise false.</returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>Checks if a username is already taken asynchronously.</summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if username exists; otherwise false.</returns>
        Task<bool> UsernameExistsAsync(string username);

        /// <summary>Updates an existing user account asynchronously.</summary>
        /// <param name="user">The user with updated information.</param>
        Task UpdateAsync(User user);
    }
}
