using Marketplace.Core.DTO;
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
    private readonly IMapperAbstraction _mapper;

    public MainCategoryController(IMainCategoryService service, IMapperAbstraction mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<MainCategoryDto>> GetMainCategories()
    {
        _service.SeedMainCategoryData(); //Temporary

        var mainCategories = _service.GetAllMainCategories();

        return (mainCategories is null || mainCategories.Count() == 0) ? 
            NoContent() : 
            Ok(_mapper.Map<IEnumerable<MainCategoryDto>>(mainCategories));
    }
}