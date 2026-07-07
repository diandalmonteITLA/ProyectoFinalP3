using App.Core.Application.DTOs.User;

namespace App.Core.Application.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponseDto> AuthenticateAsync(LoginDto loginDto);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<UserResponseDto> DeleteAsync(string id);
        Task<EditResponseDto> EditUser(RegisterUserDto registerUser, string origin, bool? isCreated = false);
        Task<List<UserDto>> GetAllUser(bool? isActive = true);
        Task<UserDto?> GetUserById(string Id);
        Task<UserDto?> GetUserByUserName(string userName);
        Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUser, string origin);
        Task SignOutAsync();
    }
}