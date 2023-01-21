using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces.Services;
using Marketplace.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainCategoryController : Controller
{
    private readonly IMainCategoryService _service;
    private readonly IAutoMapper _mapper;

    public MainCategoryController(IMainCategoryService service, IAutoMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<MainCategory>>> GetMainCategories()
    {
        //_service.SeedData();

        var mainCategories = _service.GetAllMainCategories();

        if (mainCategories is null || mainCategories.Count() == 0)
        {
            return NoContent();
        }


    }
}