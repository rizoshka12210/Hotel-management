using Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }



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

}