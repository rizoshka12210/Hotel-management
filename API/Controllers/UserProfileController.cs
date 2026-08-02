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

}