using AutoMapper;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Marketplace.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet("Get/{categoryRoute}"), AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Get([FromRoute] string categoryRoute, [FromQuery] int pageNumber)
    {
        if (string.IsNullOrEmpty(categoryRoute))
        {
            return BadRequest("Malformed request syntax");
        }

        var categoryName = categoryRoute.ToCategoryName();

        var products = _service.GetProductsByCategoryAndPage(categoryName, pageNumber);

        if (products is null || !products.Any())
        {
            return NoContent();
        }

        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        foreach (var pr in products)
        {
            var dto = dtos.First(d => d.Id == pr.Id);
            dto.PriceInfo = _service.GetPriceInfoIfExists(pr);
            dto.Condition = _service.GetConditionIfExists(pr);
        }

        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Ok(dtos);
        }

        var likedProductIds = _service.GetLikedProductIds(products, userStsId);

        if (likedProductIds == null)
        {
            return BadRequest("User does not exist");
        }

        return (likedProductIds.Any()) ? Ok(new { dtos, likedProductIds }) : Ok(dtos);
    }

    [HttpGet("{productId}"), AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetById([FromRoute] int productId)
    {
        var product = _service.GetProductById(productId);

        if (product == null)
        {
            return NoContent();
        }

        string priceInfo = _service.GetPriceInfoIfExists(product, removeTags: true);

        var dto = _mapper.Map<ProductDetailsDto>(product);

        dto.PriceInfo = priceInfo;

        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Ok(dto);
        }

        bool isLiked = _service.IsProductLiked(product, userStsId);

        dto.IsLiked = isLiked;

        return Ok(dto);
    }

    [HttpGet("GetCount/{categoryRoute}"), AllowAnonymous]
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

    [HttpPost, Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Create([FromBody] CreateProductDto productDto)
    {
        string userStsId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userStsId))
        {
            return Unauthorized();
        }

        if (productDto.TagValuesDictionary is null)
        {
            return BadRequest("Missing tag values");
        }

        var product = _mapper.Map<Product>(productDto);

        var success = _service.CreateProductWithTagValues(product, productDto.TagValuesDictionary, productDto.CategoryId, userStsId);

        if (!success)
        {
            return BadRequest("Error while adding product to DB");
        }

        return Ok();
    }
}