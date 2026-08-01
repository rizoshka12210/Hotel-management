using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;


    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;


    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;
}