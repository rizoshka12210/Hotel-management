<<<<<<< HEAD
using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    DateTime GetAccessTokenExpiration();
=======
public interface IJwtService
{
    string GenerateToken(User user);
>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
}