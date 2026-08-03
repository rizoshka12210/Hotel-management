using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        

        services.AddDbContext<AppDbContext>(options =>
        {

            options.UseNpgsql(
                configuration.GetConnectionString(
                    "DefaultConnection"
                ));

        });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserProfileService, UserProfileService>();

        return services;
    }

}