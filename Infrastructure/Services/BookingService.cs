using Application.DTOs.Bookings;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto> CreateAsync(
        int userId,
        CreateBookingDto dto)
    {
        if (dto.StartDate >= dto.EndDate)
        {
            throw new ArgumentException(
                "End date must be later than start date.");
        }

        if (dto.StartDate < DateTime.UtcNow)
        {
            throw new ArgumentException(
                "Start date cannot be in the past.");
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == dto.RoomId);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        if (room.Status == RoomStatus.Maintenance)
        {
            throw new InvalidOperationException(
                "Room is under maintenance.");
        }

        var hasConflict = await _context.Bookings
            .AnyAsync(x =>
                x.RoomId == dto.RoomId &&
                x.Status != BookingStatus.Cancelled &&
                dto.StartDate < x.EndDate &&
                dto.EndDate > x.StartDate);

        if (hasConflict)
        {
            throw new InvalidOperationException(
                "Room is already booked for these dates.");
        }

        var days = (dto.EndDate - dto.StartDate).Days;

        var totalPrice = days * room.Price;

        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = userId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = totalPrice,
            Status = BookingStatus.Pending
        };

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        return MapToDto(booking);
    }

    public async Task CancelAsync(
        int bookingId,
        int userId)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(x =>
                x.Id == bookingId &&
                x.UserId == userId);

        if (booking == null)
        {
            throw new KeyNotFoundException(
                "Booking not found.");
        }

        if (booking.Status == BookingStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Booking is already cancelled.");
        }

        booking.Status = BookingStatus.Cancelled;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookingDto>> GetHistoryAsync(
        int userId)
    {
        var bookings = await _context.Bookings
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return bookings.Select(MapToDto);
    }

    private static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status.ToString(),
            CreatedAt = booking.CreatedAt
        };
    }
}