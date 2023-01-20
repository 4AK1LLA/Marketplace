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
    public async Task<ActionResult<IEnumerable<MainCategory>>> GetMainCategories()
    {

    }
}