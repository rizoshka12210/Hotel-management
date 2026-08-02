using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Rooms;

public class CreateRoomDto
{
    [Required]
    public int HotelId { get; set; }

    [Required]
    public int Number { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = null!;
}