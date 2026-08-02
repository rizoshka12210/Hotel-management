using Application.DTOs.Rooms;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<RoomDto> CreateAsync(CreateRoomDto dto);

    Task<RoomDto> GetByIdAsync(int id);

    Task<IEnumerable<RoomDto>> GetAllAsync(
        int hotelId,
        int page,
        int pageSize);

    Task<RoomDto> UpdateAsync(
        int id,
        UpdateRoomDto dto);

    Task DeleteAsync(int id);
}