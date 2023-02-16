using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Marketplace.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductController(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("Get/{categoryRoute}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<ProductDto>> Get([FromRoute] string categoryRoute, [FromQuery] int pageNumber)
    {
        var categoryName = categoryRoute.ToCategoryName();

        var products = _service.GetProductsByCategoryAndPage(categoryName, pageNumber);

        return (products is null || products.Count() == 0) ?
            NoContent() :
            Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("GetCount/{categoryRoute}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<int> GetCount([FromRoute] string categoryRoute)
    {
        //Mapping route to name
        var str = categoryRoute.Replace('-', ' ');
        var categoryName = char.ToUpper(str[0]) + str.Substring(1);

        var count = _service.GetProductsCountByCategory(categoryName);

        return Ok(count);
    }

    //TODO: create product endpoint
}