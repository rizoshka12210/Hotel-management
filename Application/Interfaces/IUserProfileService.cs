using Application.DTOs.Users;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetProfile(int userId);
    Task<bool> ChangePassword(int userId, ChangePasswordDto dto);
    Task<bool> UpdateProfile(int userId, UpdateProfileDto dto);
    Task<List<UserDto>> GetAllUsers();
    Task<bool> DeleteUser(int userId);
}