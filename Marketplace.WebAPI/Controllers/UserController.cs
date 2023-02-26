using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Create()
    {
        //TODO: Add assignment of email claim

        var httpUser = HttpContext.User;

        if (httpUser is null)
        {
            return BadRequest("Missing claim principal");
        }

        var identifierClaim = httpUser.FindFirst(ClaimTypes.NameIdentifier);

        if (identifierClaim is null)
        {
            return BadRequest("Missing identifier claim");
        }

        if (_service.GetUserByIdentifier(identifierClaim.Value) is not null)
        {
            return NoContent();
        }

        var dto = new CreateUserDto
        {
            StsIdentifier = identifierClaim.Value,
            UserName = httpUser.FindFirstValue(ClaimTypes.Name),
            DisplayName = httpUser.FindFirstValue("display_name"),
            ProfilePictureUrl = httpUser.FindFirstValue("profile_picture")
        };

        if (httpUser.FindFirst("display_name") is null)
        {
            dto.DisplayName = dto.UserName;
        }

        _service.AddUser(_mapper.Map<AppUser>(dto));

        return Ok(dto);
    }
}
