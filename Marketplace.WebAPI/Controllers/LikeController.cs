using AutoMapper;
using Marketplace.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeController : ControllerBase
{
    private readonly IProductService _service;

    public LikeController(IProductService service, IMapper mapper)
    {
        _service = service;
    }

    [HttpPost("{productId}"), Authorize]
    public IActionResult Like([FromRoute] int productId)
    {
        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Unauthorized();
        }

        bool success = _service.LikeProduct(productId, userStsId);

        return (success) ? Ok() : BadRequest("Error while like/dislike");
    }
}