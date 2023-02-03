using AutoMapper;
using FluentAssertions;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.Controllers;
using Marketplace.WebAPI.DTO;
using Marketplace.WebAPI.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MarketplaceWebAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productService;

        public ProductControllerTests()
        {
            _productService = new Mock<IProductService>();

            var mapper = new Mapper(new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()))) as IMapper;

            _productController = new ProductController(_productService.Object, mapper);
        }

        [Fact]
        public void Get_ReturnOk_WhenProductsAreNotEmpty()
        {
            _productService
                .Setup(ps => ps.GetProductsByCategory("CategoryName"))
                .Returns(new List<Product> {
                    new Product(), new Product(), new Product()
                });

            var products = _productController.Get("CategoryName");

            var result = products.Result as ObjectResult;
            var value = result!.Value as IEnumerable<ProductDto>;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            value.Should().NotBeNullOrEmpty()
                .And.HaveCount(3)
                .And.AllBeOfType<ProductDto>();
        }

        [Fact]
        public void Get_ReturnNoContent_WhenProductsAreEmpty()
        {
            _productService
                .Setup(ps => ps.GetProductsByCategory("CategoryName"))
                .Returns(Enumerable.Empty<Product>);

            var products = _productController.Get("CategoryName");

            (products.Result as StatusCodeResult)!.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}