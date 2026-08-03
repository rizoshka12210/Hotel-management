using Application.DTOs.Rooms;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RoomDto>> Create(
        [FromBody] CreateRoomDto dto)
    {
        var room = await _roomService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = room.Id },
            room);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);

        return Ok(room);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll(
        [FromQuery] int hotelId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var rooms = await _roomService.GetAllAsync(
            hotelId,
            page,
            pageSize);

        return Ok(rooms);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RoomDto>> Update(
        int id,
        [FromBody] UpdateRoomDto dto)
    {
        var room = await _roomService.UpdateAsync(id, dto);

        return Ok(room);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roomService.DeleteAsync(id);

        return NoContent();
    }
}