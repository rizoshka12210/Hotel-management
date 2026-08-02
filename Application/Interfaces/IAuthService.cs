using Application.DTOs.Auth;
public interface IAuthService
{
    Task<bool> Register(RegisterDto dto);
    Task<string?> Login(LoginDto dto);
}