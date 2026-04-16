using BCrypt.Net;
using UserService.Core.DTOs;
using UserService.Core.Entities;
using UserService.Core.Interfaces;

namespace UserService.API.Services
{
    public class UserBusinessService : IUserBusinessService
    {
        private readonly IUserRepository _repository;

        public UserBusinessService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<UserResponse>> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                if (await _repository.EmailExistsAsync(request.Email))
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "Email already exists",
                        ErrorCode = "EMAIL_EXISTS"
                    };
                }

                if (await _repository.UsernameExistsAsync(request.Username))
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "Username already taken",
                        ErrorCode = "USERNAME_EXISTS"
                    };
                }

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                var created = await _repository.CreateAsync(user);

                return new ApiResponse<UserResponse>
                {
                    Success = true,
                    Message = "User created successfully",
                    Data = new UserResponse
                    {
                        Id = created.Id,
                        Username = created.Username,
                        Email = created.Email,
                        CreatedAt = created.CreatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = $"Failed to create user: {ex.Message}",
                    ErrorCode = "CREATE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<UserResponse>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "User not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                return new ApiResponse<UserResponse>
                {
                    Success = true,
                    Message = "User retrieved successfully",
                    Data = new UserResponse
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = $"Failed to retrieve user: {ex.Message}",
                    ErrorCode = "RETRIEVE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<UserResponse>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _repository.GetByEmailAsync(email);
                if (user == null)
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "User not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                return new ApiResponse<UserResponse>
                {
                    Success = true,
                    Message = "User retrieved successfully",
                    Data = new UserResponse
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = $"Failed to retrieve user: {ex.Message}",
                    ErrorCode = "RETRIEVE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<UserResponse>> UpdateProfileAsync(Guid userId, UpdateProfileRequest request)
        {
            try
            {
                var user = await _repository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "User not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                if (user.Email != request.Email && await _repository.EmailExistsAsync(request.Email, userId))
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "Email already taken",
                        ErrorCode = "EMAIL_EXISTS"
                    };
                }

                if (user.Username != request.Username && await _repository.UsernameExistsAsync(request.Username, userId))
                {
                    return new ApiResponse<UserResponse>
                    {
                        Success = false,
                        Message = "Username already taken",
                        ErrorCode = "USERNAME_EXISTS"
                    };
                }

                user.Username = request.Username;
                user.Email = request.Email;
                
                var updated = await _repository.UpdateAsync(user);

                return new ApiResponse<UserResponse>
                {
                    Success = true,
                    Message = "Profile updated successfully",
                    Data = new UserResponse
                    {
                        Id = updated.Id,
                        Username = updated.Username,
                        Email = updated.Email,
                        CreatedAt = updated.CreatedAt,
                        UpdatedAt = updated.UpdatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = $"Failed to update profile: {ex.Message}",
                    ErrorCode = "UPDATE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
        {
            try
            {
                var user = await _repository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "User not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Current password is incorrect",
                        ErrorCode = "INVALID_PASSWORD"
                    };
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _repository.UpdateAsync(user);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Password changed successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Failed to change password: {ex.Message}",
                    ErrorCode = "PASSWORD_ERROR"
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid userId)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(userId);
                if (!deleted)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "User not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "User deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Failed to delete user: {ex.Message}",
                    ErrorCode = "DELETE_ERROR"
                };
            }
        }

        public async Task<ValidateUserResponse> ValidateUserCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _repository.GetByEmailAsync(email);
                if (user == null)
                {
                    return new ValidateUserResponse
                    {
                        IsValid = false,
                        ErrorMessage = "Invalid email or password"
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new ValidateUserResponse
                    {
                        IsValid = false,
                        ErrorMessage = "Invalid email or password"
                    };
                }

                return new ValidateUserResponse
                {
                    IsValid = true,
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };
            }
            catch (Exception ex)
            {
                return new ValidateUserResponse
                {
                    IsValid = false,
                    ErrorMessage = $"Validation error: {ex.Message}"
                };
            }
        }
    }
}