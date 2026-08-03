using Application.DTOs.Bookings;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<BookingDto> CreateAsync(
        int userId,
        CreateBookingDto dto);

    Task CancelAsync(
        int bookingId,
        int userId);

    Task<IEnumerable<BookingDto>> GetHistoryAsync(
        int userId);
}