using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _service;
    private readonly IMapper _mapper;

    public TagController(ITagService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TagDto>> Get([FromQuery] int? categoryId)
    {
        if (!categoryId.HasValue) 
        {
            return NoContent();
        }

        var tags = _service.GetTagsByCategoryId(categoryId.Value);

        return (tags is null || !tags.Any()) ?
            NoContent() :
            Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }
}
