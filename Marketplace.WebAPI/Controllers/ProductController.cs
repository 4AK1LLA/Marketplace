using Marketplace.Core.Entities;
using Marketplace.Data;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly MarketplaceContext context;

    public ProductController(MarketplaceContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<GetProductDto>? GetAllProducts()
    {
        #region ADDING PRODUCT
        if (context.Database.EnsureCreated())
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

            context.Products?.Add(product);
            context.SaveChanges();
        }
        #endregion

        var products = context.Products?.ToList(); //Income products do not contain related categories

        var result = new List<GetProductDto>();
        foreach (var pr in products)
        {
            var productCategories = context.Categories
                .Where(c => c.Products.Contains(pr))
                .ToList();

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

    public class GetProductDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public DateTime PublicationDate { get; set; }

        public string? Location { get; set; }

        public ICollection<string>? Categories { get; set; }
    }
}