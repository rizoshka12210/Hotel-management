using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users;

public class UpdateUserRoleDto
{
    [Required]
    public int RoleId { get; set; }
}