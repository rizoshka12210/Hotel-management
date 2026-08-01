using Application.DTOs.Users;

namespace Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task UpdateRoleAsync(int userId, UpdateUserRoleDto dto);
    Task DeleteAsync(int id);
}