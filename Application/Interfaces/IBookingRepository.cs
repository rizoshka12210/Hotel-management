using Domain.Entities;

namespace Application.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);

    Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);

    Task AddAsync(Booking booking);

    Task SaveChangesAsync();
}