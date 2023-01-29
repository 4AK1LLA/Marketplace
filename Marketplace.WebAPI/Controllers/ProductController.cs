using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
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

    [HttpGet("{categoryRoute}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<ProductDto>> Get(string categoryRoute)
    {
        //Mapping route to name
        var str = categoryRoute.Replace('-', ' ');
        var categoryName = char.ToUpper(str[0]) + str.Substring(1);

        var products = _service.GetProductsByCategory(categoryName);

        return (products is null || products.Count() == 0) ?
            NoContent() :
            Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    //TODO: create product endpoint
}