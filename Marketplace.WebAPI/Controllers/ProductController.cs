using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Marketplace.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[AllowAnonymous]
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ProductDto>> Get([FromRoute] string categoryRoute, [FromQuery] int pageNumber)
    {
        if (string.IsNullOrEmpty(categoryRoute))
        {
            return BadRequest("Malformed request syntax");
        }

        var categoryName = categoryRoute.ToCategoryName();

        var products = _service.GetProductsByCategoryAndPage(categoryName, pageNumber);

        if (products is null)
        {
            return NotFound();
        }

        return (products.Count() == 0) ?
            NoContent() :
            Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{productId}")]
    public ActionResult<ProductDetailsDto> GetById([FromRoute] int productId)
    {
        var product = _service.GetProductById(productId);

        var dto = _mapper.Map<ProductDetailsDto>(product);

        return (product is null) ?
            NoContent() :
            Ok(_mapper.Map<ProductDetailsDto>(product));
    }

    [HttpGet("GetCount/{categoryRoute}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<int> GetCount([FromRoute] string categoryRoute)
    {
        if (string.IsNullOrEmpty(categoryRoute))
        {
            return BadRequest("Malformed request syntax");
        }

        var categoryName = categoryRoute.ToCategoryName();

        var count = _service.GetProductsCountByCategory(categoryName);

        return Ok(count);
    }

    //TODO: create product endpoint
}