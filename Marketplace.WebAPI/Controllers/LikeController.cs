using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.Shared.Generic;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public LikeController(IProductService productService, IUserService userService, IMapper mapper)
    {
        _productService = productService;
        _userService = userService;
        _mapper = mapper;
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

        Result<bool> response = _productService.LikeProduct(productId, userStsId);

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

        var likedProducts = _productService.GetLikedProducts(userStsId);

        return (likedProducts is not null && likedProducts.Any()) 
            ? Ok(_mapper.Map<IEnumerable<ProductDto>>(likedProducts)) 
            : NoContent();
    }

    [HttpDelete, Authorize]
    public IActionResult RemoveAllLikes()
    {
        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Unauthorized();
        }

        bool success = _userService.RemoveAllLikes(userStsId);

        return Ok(success);
    }
}