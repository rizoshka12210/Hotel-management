using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Auth;
public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext context,IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var userExists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email);

        if (userExists)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = 2
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var role = await _context.Roles
            .FirstAsync(x => x.Id == user.RoleId);

        user.Role = role;

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null)
        {
            throw new Exception("Invalid email or password.");
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
     dto.Password,
     user.PasswordHash);

        if (!passwordValid)
        {
            throw new Exception("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new Exception("User is inactive.");
        }

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(
        RefreshTokenDto dto)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(x => x.User)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.Token == dto.RefreshToken);

        if (refreshToken == null)
        {
            throw new Exception("Invalid refresh token.");
        }

        if (refreshToken.IsRevoked)
        {
            throw new Exception("Refresh token has been revoked.");
        }

        if (refreshToken.ExpireDate <= DateTime.UtcNow)
        {
            throw new Exception("Refresh token has expired.");
        }

        refreshToken.IsRevoked = true;

        var response = await CreateAuthResponseAsync(refreshToken.User);

        await _context.SaveChangesAsync();

        return response;
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .Include(x => x.User)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (token == null || token.User == null)
        {
            return;
        }

        token.IsRevoked = true;

        await _context.SaveChangesAsync();
    }

    private async Task<AuthResponseDto> CreateAuthResponseAsync(User user)
    {
        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenExpiration =
            DateTime.UtcNow.AddDays(7);

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpireDate = refreshTokenExpiration,
            IsRevoked = false,
            UserId = user.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenEntity);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = _jwtService.GetAccessTokenExpiration()
        };
    }
}