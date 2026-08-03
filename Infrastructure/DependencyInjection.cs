using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
<<<<<<< HEAD
=======
        

>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            );
        });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
<<<<<<< HEAD
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IRoomService, RoomService>();
=======
        services.AddScoped<IUserProfileService, UserProfileService>();
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25

        return services;
    }
}