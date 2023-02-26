using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<string> Create()
    {
        var claimUser = HttpContext.User.Identity;

        if (claimUser is null)
        {
            return BadRequest("Missing claim principal");
        }

        return Ok("User created");
    }
}
