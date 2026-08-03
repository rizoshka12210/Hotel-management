<<<<<<< HEAD
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;
=======
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

<<<<<<< HEAD
    public string GenerateAccessToken(User user)
    {
        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;

        var expirationMinutes =
            int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"]!);

=======
    public string GenerateToken(User user)
    {
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
<<<<<<< HEAD
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
=======
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["JwtSettings:ExpireMinutes"])
            ),
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
<<<<<<< HEAD

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    public DateTime GetAccessTokenExpiration()
    {
        var expirationMinutes =
            int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"]!);

        return DateTime.UtcNow.AddMinutes(expirationMinutes);
    }
=======
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
}