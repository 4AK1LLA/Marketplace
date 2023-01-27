using AutoMapper;
using Marketplace.Core.Interfaces.Services;
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

    //TODO: change create product endpoint

    //[HttpPost]
    //public GetProductDto CreateProduct(CreateProductDto dto)
    //{
    //    var product = new Product
    //    {
    //        Title = dto.Title,
    //        Description = dto.Description,
    //        Location = dto.Location,
    //        PublicationDate = DateTime.Now,
    //        Category = _uow.CategoryRepository.Get(dto.CategoryId),
    //        TagValues = new List<TagValue>()
    //    };

    //    if (dto.TagIdsAndValues != null)
    //        foreach (var item in dto.TagIdsAndValues)
    //        {
    //            product.TagValues.Add(new TagValue
    //            {
    //                Value = item.Value,
    //                Tag = _uow.TagRepository.Get(item.Key)
    //            });
    //        }

    //    _uow.ProductRepository.Add(product);
    //    _uow.Save();

    //    #region RETURNING_CREATED_PRODUCT

    //    var receivedProduct =
    //        _uow.ProductRepository
    //        .GetIncludingCategoryAndTagValues(product.Id);

    //    var viewDto = new GetProductDto
    //    {
    //        Id = receivedProduct.Id,
    //        Title = receivedProduct.Title,
    //        Description = receivedProduct.Description,
    //        Location = receivedProduct.Location,
    //        PublicationDate = receivedProduct.PublicationDate,
    //        Category = receivedProduct.Category!.Name,
    //        TagNamesAndValues = new Dictionary<string, string>()
    //    };

    //    if (receivedProduct.TagValues != null)
    //        foreach (var item in receivedProduct.TagValues)
    //        {
    //            viewDto.TagNamesAndValues.Add(item.Tag!.Name!, item.Value!);
    //        }

    //    #endregion
    //    return viewDto;
    //}
}