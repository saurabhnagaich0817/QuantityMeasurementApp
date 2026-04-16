using UserService.Core.DTOs;

namespace UserService.Core.Interfaces
{
    public interface IUserBusinessService
    {
        Task<ApiResponse<UserResponse>> CreateUserAsync(CreateUserRequest request);
        Task<ApiResponse<UserResponse>> GetUserByIdAsync(Guid id);
        Task<ApiResponse<UserResponse>> GetUserByEmailAsync(string email);
        Task<ApiResponse<UserResponse>> UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
        Task<ApiResponse<bool>> ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
        Task<ApiResponse<bool>> DeleteUserAsync(Guid userId);
        Task<ValidateUserResponse> ValidateUserCredentialsAsync(string email, string password);
    }
}