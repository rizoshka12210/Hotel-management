using Application.DTOs.Hotels;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<HotelDto>> Create(
        [FromBody] CreateHotelDto dto)
    {
        var hotel = await _hotelService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = hotel.Id },
            hotel);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HotelDto>> GetById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);

        return Ok(hotel);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] double? ratingFrom = null,
        [FromQuery] double? ratingTo = null,
        [FromQuery] string? sort = null)
    {
        var hotels = await _hotelService.GetAllAsync(
            page,
            pageSize,
            search,
            ratingFrom,
            ratingTo,
            sort);

        return Ok(hotels);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<HotelDto>> Update(
        int id,
        [FromBody] UpdateHotelDto dto)
    {
        var hotel = await _hotelService.UpdateAsync(id, dto);

        return Ok(hotel);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _hotelService.DeleteAsync(id);

        return NoContent();
    }
}