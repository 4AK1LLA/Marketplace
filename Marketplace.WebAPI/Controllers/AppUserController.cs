using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppUserController : Controller
{
    [HttpPost]
    public IActionResult Create()
    {
        return Ok();
    }
}
