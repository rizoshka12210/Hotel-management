using Application.DTOs.Hotels;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class HotelService : IHotelService
{
    private readonly AppDbContext _context;

    public HotelService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HotelDto> CreateAsync(CreateHotelDto dto)
    {
        var hotel = new Hotel
        {
            Name = dto.Name,
            Address = dto.Address,
            Description = dto.Description,
            Rating = dto.Rating
        };

        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();

        return MapToDto(hotel);
    }

    public async Task<HotelDto> GetByIdAsync(int id)
    {
        var hotel = await _context.Hotels
            .FirstOrDefaultAsync(x => x.Id == id);

        if (hotel == null)
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        return MapToDto(hotel);
    }

    public async Task<IEnumerable<HotelDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        double? ratingFrom,
        double? ratingTo,
        string? sort)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = _context.Hotels
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.Name.Contains(search) ||
                x.Address.Contains(search));
        }

        if (ratingFrom.HasValue)
        {
            query = query.Where(x =>
                x.Rating >= ratingFrom.Value);
        }

        if (ratingTo.HasValue)
        {
            query = query.Where(x =>
                x.Rating <= ratingTo.Value);
        }

        query = sort?.ToLower() switch
        {
            "ratingasc" => query.OrderBy(x => x.Rating),
            "ratingdesc" => query.OrderByDescending(x => x.Rating),
            "nameasc" => query.OrderBy(x => x.Name),
            "namedesc" => query.OrderByDescending(x => x.Name),
            _ => query.OrderBy(x => x.Id)
        };

        var hotels = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return hotels.Select(MapToDto);
    }

    public async Task<HotelDto> UpdateAsync(
        int id,
        UpdateHotelDto dto)
    {
        var hotel = await _context.Hotels
            .FirstOrDefaultAsync(x => x.Id == id);

        if (hotel == null)
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        hotel.Name = dto.Name;
        hotel.Address = dto.Address;
        hotel.Description = dto.Description;
        hotel.Rating = dto.Rating;

        await _context.SaveChangesAsync();

        return MapToDto(hotel);
    }

    public async Task DeleteAsync(int id)
    {
        var hotel = await _context.Hotels
            .FirstOrDefaultAsync(x => x.Id == id);

        if (hotel == null)
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        _context.Hotels.Remove(hotel);

        await _context.SaveChangesAsync();
    }

    private static HotelDto MapToDto(Hotel hotel)
    {
        return new HotelDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Description = hotel.Description,
            Rating = hotel.Rating
        };
    }
}