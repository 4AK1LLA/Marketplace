using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class MainCategoryController : Controller
{
    private readonly IMainCategoryService _service;
    private readonly IMapper _mapper;

    public MainCategoryController(IMainCategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<MainCategoryDto>> GetMainCategories()
    {
        var mainCategories = _service.GetAllMainCategories();

        return (mainCategories is null || mainCategories.Count() == 0) ? 
            NoContent() : 
            Ok(_mapper.Map<IEnumerable<MainCategoryDto>>(mainCategories));
    }
}