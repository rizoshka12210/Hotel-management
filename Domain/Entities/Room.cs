using Domain.Common;

namespace Domain.Entities;

public class Room : BaseEntity
{
    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = null!;

    public int Number { get; set; }

    public decimal Price { get; set; }

    public int Capacity { get; set; }

    public RoomStatus Status { get; set; }

    public string Description { get; set; } = null!;
}

public enum RoomStatus
{
    Available,
    Occupied,
    Maintenance
}