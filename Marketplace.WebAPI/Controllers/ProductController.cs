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
        var product = new Product
        {
            Name = "Apple",
            Description = "Tasty",
            Price = 25,
            PublicationDate = DateTime.Now,
            Location = "Kyiv",
            Categories = new List<Category> { new Category { Name = "Food" } }
        };

        if (context.Database.EnsureCreated())
        {
            context.Products?.Add(product);
            context.SaveChanges();
        }

        var products = context.Products?.ToList();

        List<GetProductDto> result = new List<GetProductDto>();
        foreach (var pr in products)
        {
            var productDto = new GetProductDto
            {
                Name = pr.Name,
                Description = pr.Description,
                Price = pr.Price,
                PublicationDate = pr.PublicationDate,
                Location = pr.Location
            };

            List<string> categories = new List<string>();
            foreach (var ct in pr.Categories)
            {
                categories.Add(ct.Name);
            }

            productDto.Categories = categories;

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