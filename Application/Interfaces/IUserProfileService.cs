public interface IUserProfileService
{
    Task<UserProfileDto?> GetProfile(int userId);
}