using Application.DTOs.Auth;
<<<<<<< HEAD
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
=======
using Microsoft.AspNetCore.Mvc;

>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
<<<<<<< HEAD
    private readonly IAuthService _authService;

=======

    private readonly IAuthService _authService;


>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

<<<<<<< HEAD
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenDto dto)
    {
        await _authService.LogoutAsync(dto.RefreshToken);

        return NoContent();
    }
=======


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {

        var result = await _authService.Register(dto);


        if(!result)
        {
            return BadRequest("User already exists");
        }


        return Ok("User registered successfully");
    }




    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {

        var result = await _authService.Login(dto);


        if(result == null)
        {
            return Unauthorized("Invalid email or password");
        }


        return Ok(new
        {
            message = result
        });
    }

>>>>>>> 30830c824e952ef5aa876fbd8e52ff9f4eb6ef25
}