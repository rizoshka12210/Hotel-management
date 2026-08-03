using Application.DTOs.Rooms;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomDto> CreateAsync(CreateRoomDto dto)
    {
        var hotelExists = await _context.Hotels
            .AnyAsync(x => x.Id == dto.HotelId);

        if (!hotelExists)
        {
            throw new KeyNotFoundException("Hotel not found.");
        }

        var roomExists = await _context.Rooms
            .AnyAsync(x =>
                x.HotelId == dto.HotelId &&
                x.Number == dto.Number);

        if (roomExists)
        {
            throw new InvalidOperationException(
                "A room with this number already exists in this hotel.");
        }

        var room = new Room
        {
            HotelId = dto.HotelId,
            Number = dto.Number,
            Price = dto.Price,
            Capacity = dto.Capacity,
            Status = RoomStatus.Available,
            Description = dto.Description
        };

        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();

        return MapToDto(room);
    }

    public async Task<RoomDto> GetByIdAsync(int id)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        return MapToDto(room);
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync(
        int hotelId,
        int page,
        int pageSize)
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

        var rooms = await _context.Rooms
            .AsNoTracking()
            .Where(x => x.HotelId == hotelId)
            .OrderBy(x => x.Number)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return rooms.Select(MapToDto);
    }

    public async Task<RoomDto> UpdateAsync(
        int id,
        UpdateRoomDto dto)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        var numberExists = await _context.Rooms
            .AnyAsync(x =>
                x.Id != id &&
                x.HotelId == room.HotelId &&
                x.Number == dto.Number);

        if (numberExists)
        {
            throw new InvalidOperationException(
                "A room with this number already exists in this hotel.");
        }

        room.Number = dto.Number;
        room.Price = dto.Price;
        room.Capacity = dto.Capacity;
        room.Description = dto.Description;

        await _context.SaveChangesAsync();

        return MapToDto(room);
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == id);

        if (room == null)
        {
            throw new KeyNotFoundException("Room not found.");
        }

        _context.Rooms.Remove(room);

        await _context.SaveChangesAsync();
    }

    private static RoomDto MapToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Number = room.Number,
            Price = room.Price,
            Capacity = room.Capacity,
            Status = room.Status.ToString(),
            Description = room.Description
        };
    }
}