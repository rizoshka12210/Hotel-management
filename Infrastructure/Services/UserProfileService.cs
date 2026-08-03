using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Users;
public class UserProfileService : IUserProfileService
{

    private readonly AppDbContext _context;


    public UserProfileService(AppDbContext context)
    {
        _context = context;
    }



    public async Task<UserProfileDto?> GetProfile(int userId)
    {

        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == userId);


        if(user == null)
        {
            return null;
        }


        return new UserProfileDto
        {
            Id = user.Id,

            FullName = user.FullName,

            Email = user.Email,

            Role = user.Role.Name
        };
    }

    public async Task<bool> ChangePassword(int userId, ChangePasswordDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return false;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.CurrentPassword,
            user.PasswordHash);

        if (!passwordValid)
        {
            return false;
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateProfile(int userId, UpdateProfileDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return false;
        }

        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email && x.Id != userId);

        if (emailExists)
        {
            return false;
        }

        user.FullName = dto.FullName;
        user.Email = dto.Email;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _context.Users
            .Where(x => !x.IsDeleted)
            .Include(x => x.Role)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Role = x.Role.Name,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);


        if (user == null)
        {
            return false;
        }


        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.IsActive = false;


        await _context.SaveChangesAsync();


        return true;
    }
}