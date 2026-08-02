namespace Application.DTOs.Rooms;

public class RoomDto
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public int Number { get; set; }

    public decimal Price { get; set; }

    public int Capacity { get; set; }

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;
}