using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _uow;

    public ProductController(IUnitOfWork uow) => _uow = uow;

    [HttpPost]
    public ProductDto CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            PublicationDate = DateTime.Now,
            Category = _uow.CategoryRepository.Get(dto.CategoryId),
            TagValues = new List<TagValue>()
        };

        if (dto.TagIdsAndValues != null)
            foreach (var item in dto.TagIdsAndValues)
            {
                product.TagValues.Add(new TagValue
                {
                    Value = item.Value,
                    Tag = _uow.TagRepository.Get(item.Key)
                });
            }

        _uow.ProductRepository.Add(product);
        _uow.Save();

        #region RETURNING_CREATED_PRODUCT

        var receivedProduct =
            _uow.ProductRepository
            .GetIncludingCategoryAndTagValues(product.Id);

        var viewDto = new ProductDto
        {
            Id = receivedProduct.Id,
            Title = receivedProduct.Title,
            Description = receivedProduct.Description,
            Location = receivedProduct.Location,
            PublicationDate = receivedProduct.PublicationDate,
            Category = receivedProduct.Category!.Name,
            TagNamesAndValues = new Dictionary<string, string>()
        };

        if (receivedProduct.TagValues != null)
            foreach (var item in receivedProduct.TagValues)
            {
                viewDto.TagNamesAndValues.Add(item.Tag!.Name!, item.Value!);
            }

        #endregion
        return viewDto;
    }
}