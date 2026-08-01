using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Hotels;

public class UpdateHotelDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;


    [Required]
    [MaxLength(250)]
    public string Address { get; set; } = null!;


    [MaxLength(1000)]
    public string Description { get; set; } = null!;


    [Range(0, 5)]
    public double Rating { get; set; }
}