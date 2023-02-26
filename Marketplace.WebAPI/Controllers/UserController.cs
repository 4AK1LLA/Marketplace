using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Create()
    {
        var claimUser = HttpContext.User.Identity;

        if (claimUser is null)
        {
            return BadRequest("Missing claim principal");
        }

        var dto = new CreateUserDto
        {
            //StsIdentifier = 
        };

        return Ok("User created");
    }
}
