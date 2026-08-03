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



    public async Task<bool> Register(RegisterDto dto)
    {

        var userExists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email);


        if(userExists)
        {
            return false;
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


        return true;
    }




    public async Task<string?> Login(LoginDto dto)
    {

        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == dto.Email);



        if(user == null)
        {
            return null;
        }



        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash
        );


        if(!passwordValid)
        {
            return null;
        }



        return _jwtService.GenerateToken(user);
    }

    public async Task<bool> Logout(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (token == null)
        {
            return false;
        }

        token.IsRevoked = true;

        await _context.SaveChangesAsync();

        return true;
    }
}