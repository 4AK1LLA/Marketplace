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
    public IEnumerable<Product>? GetAllProducts()
    {
        return null;
    }
}