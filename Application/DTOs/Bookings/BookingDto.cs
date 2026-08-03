namespace Application.DTOs.Bookings;

public class BookingDto
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}