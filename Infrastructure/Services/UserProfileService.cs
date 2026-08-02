using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

}