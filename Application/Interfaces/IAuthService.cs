using Application.DTOs.Auth;
<<<<<<< HEAD

namespace Application.Interfaces;

=======
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task LogoutAsync(string refreshToken);
}
