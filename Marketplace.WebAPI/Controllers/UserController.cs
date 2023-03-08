using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Shared.Claims;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<GetUserDto> GetOrCreate([FromHeader] string idToken)
    {
        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(idToken))
        {
            return BadRequest("Failed to parse id token");
        }

        IEnumerable<Claim> claims = handler.ReadJwtToken(idToken).Claims;
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        var userName = principal.FindFirstValue(AppClaimTypes.name.ToString());
        var foundUser = _service.GetUserByName(userName);

        if (foundUser is not null)
        {
            return Ok(_mapper.Map<GetUserDto>(foundUser));
        }

        var dto = new CreateUserDto
        {
            UserName = userName,
            StsIdentifier = principal.FindFirstValue(AppClaimTypes.sub.ToString()),
            Email = principal.FindFirstValue(AppClaimTypes.email.ToString()),
            DisplayName = principal.FindFirstValue(AppClaimTypes.display_name.ToString()),
            ProfilePictureUrl = principal.FindFirstValue(AppClaimTypes.profile_picture.ToString())
        };

        _service.AddUser(_mapper.Map<AppUser>(dto));

        return Ok(_mapper.Map<GetUserDto>(dto));
    }
}
