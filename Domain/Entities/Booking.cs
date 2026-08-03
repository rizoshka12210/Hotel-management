using Domain.Common;

namespace Domain.Entities;

public class Booking : BaseEntity
{
    public int RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public BookingStatus Status { get; set; }

  
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled
}