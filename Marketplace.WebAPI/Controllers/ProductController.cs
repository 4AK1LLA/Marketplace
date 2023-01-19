using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.Data;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ProductController(IUnitOfWork uow) => _uow = uow;

    [HttpGet]
    public IEnumerable<GetProductDto>? GetAllProducts()
    {
        #region ADDING PRODUCT
        if (_uow.ProductRepository.GetAll().Count() == 0)
        {
            var product = new Product
            {
                Name = "Apple",
                Description = "Tasty",
                Price = 25,
                PublicationDate = DateTime.Now,
                Location = "Kyiv",
                Categories = new List<Category> { new Category { Name = "Food" } }
            };

            _uow.ProductRepository.Add(product);
            _uow.Save();
        }
        #endregion

        var products = _uow.ProductRepository.GetAll(); //Income products do not contain related categories

        var result = new List<GetProductDto>();
        foreach (var pr in products)
        {
            var productCategories = _uow.CategoryRepository.Find(c => c.Products.Contains(pr));

            var categoryNames = new List<string>();

            foreach (var ct in productCategories)
                categoryNames.Add(ct.Name);

            var productDto = new GetProductDto
            {
                Id = pr.Id,
                Name = pr.Name,
                Description = pr.Description,
                Price = pr.Price,
                PublicationDate = pr.PublicationDate,
                Location = pr.Location,
                Categories = categoryNames
            };

            result.Add(productDto);
        }

        return result;
    }
}