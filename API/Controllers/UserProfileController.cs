using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserProfileController : ControllerBase
{

    private readonly IUserProfileService _userService;


    public UserProfileController(IUserProfileService userService)
    {
        _userService = userService;
    }



    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {

        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );


        if(userId == null)
        {
            return Unauthorized();
        }


        var user = await _userService.GetProfile(
            int.Parse(userId)
        );


        if(user == null)
        {
            return NotFound();
        }


        return Ok(user);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _userService.ChangePassword(
            int.Parse(userId),
            dto);

        if (!result)
        {
            return BadRequest("Current password is incorrect");
        }

        return Ok("Password changed successfully");
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _userService.UpdateProfile(
            int.Parse(userId),
            dto);

        if (!result)
        {
            return BadRequest("Email already exists");
        }

        return Ok("Profile updated successfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();

        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUser(id);
    
    
        if (!result)
        {
            return NotFound("User not found");
        }
    
    
        return Ok("User deleted successfully");
    }
}