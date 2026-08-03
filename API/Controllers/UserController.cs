using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        return Ok(new
        {
            message = "You are authorized"
        });
    }

}