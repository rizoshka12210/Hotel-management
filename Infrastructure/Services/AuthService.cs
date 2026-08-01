namespace Infrastructure.Services;
using BCrypt.Net;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public class AuthService : IAuthService
{

    private readonly AppDbContext _context;


    public AuthService(AppDbContext context)
    {
        _context = context;
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

            PasswordHash = BCrypt.HashPassword(dto.Password),

            RoleId = 2
        };


        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();


        return true;
    }




    public async Task<string?> Login(LoginDto dto)
    {

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);



        if(user == null)
        {
            return null;
        }



        var passwordValid = BCrypt.Verify(
            dto.Password,
            user.PasswordHash
        );


        if(!passwordValid)
        {
            return null;
        }



        return "LOGIN_SUCCESS";
    }

}