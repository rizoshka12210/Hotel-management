using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public List<RefreshToken> RefreshTokens { get; set; } = new();

    public List<Booking> Bookings { get; set; } = new();

    public bool IsActive { get; set; } = true;
}