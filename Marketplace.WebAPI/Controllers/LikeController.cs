using Marketplace.Core.Interfaces;
using Marketplace.Shared.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeController : ControllerBase
{
    private readonly IProductService _service;

    public LikeController(IProductService service)
    {
        _service = service;
    }

    [HttpPut("{productId}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Like([FromRoute] int productId)
    {
        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Unauthorized();
        }

        Result<bool> response = _service.LikeProduct(productId, userStsId);

        return (string.IsNullOrEmpty(response.ErrorMessage)) ? Ok(response.Value) : BadRequest(response.ErrorMessage);
    }

    [HttpGet, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetLikedProducts()
    {
        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Unauthorized();
        }

        var likedProducts = _service.GetLikedProducts(userStsId);

        return (likedProducts is not null && likedProducts.Any()) 
            ? Ok(likedProducts) 
            : NoContent();
    }
}