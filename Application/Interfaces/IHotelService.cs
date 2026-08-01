using Application.DTOs.Hotels;

namespace Application.Interfaces;

public interface IHotelService
{
    Task<HotelDto> CreateAsync(CreateHotelDto dto);
    Task<HotelDto> GetByIdAsync(int id);
    Task<IEnumerable<HotelDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        double? ratingFrom,
        double? ratingTo,
        string? sort);

    Task<HotelDto> UpdateAsync(int id, UpdateHotelDto dto);
    Task DeleteAsync(int id);
}