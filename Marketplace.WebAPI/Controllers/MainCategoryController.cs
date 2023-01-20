using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainCategoryController : Controller
{
    private readonly IMainCategoryService _service;

    public MainCategoryController(IMainCategoryService service) => _service = service;

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