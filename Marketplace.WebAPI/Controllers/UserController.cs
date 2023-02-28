using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
    public IActionResult Create([FromHeader] string idToken)
    {
        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(idToken))
        {
            return BadRequest("Failed to parse id token");
        }

        var claims = handler.ReadJwtToken(idToken).Claims;

        var userName = claims.FirstOrDefault(c => c.Type == "name")!.Value;

        if (_service.GetUserByName(userName) is not null)
        {
            return Ok();
        }

        var identifier = claims.FirstOrDefault(c => c.Type == "sub")!.Value;

        var dto = new CreateUserDto
        {
            StsIdentifier = identifier,
            UserName = userName
        };

        var emailClaim = claims.FirstOrDefault(c => c.Type == "email");
        var displayNameClaim = claims.FirstOrDefault(c => c.Type == "display_name");
        var profilePictureClaim = claims.FirstOrDefault(c => c.Type == "profile_picture");

        if (emailClaim is not null)
        {
            dto.Email = emailClaim.Value;
        }
        if (displayNameClaim is not null)
        {
            dto.DisplayName = displayNameClaim.Value;
        }
        if (profilePictureClaim is not null)
        {
            dto.ProfilePictureUrl = profilePictureClaim.Value;
        }

        _service.AddUser(_mapper.Map<AppUser>(dto));

        return Ok(dto);
    }
}
