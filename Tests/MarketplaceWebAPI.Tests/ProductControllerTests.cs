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
        private const int validPage = 1;
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productService;

        public ProductControllerTests()
        {
            _productService = new Mock<IProductService>();

            var mapper = new Mapper(new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()))) as IMapper;

            _productController = new ProductController(_productService.Object, mapper);
        }

        [Fact]
        public void GetCount_ReturnOk_WhenProductsAreNotEmpty()
        {
            _productService
                .Setup(ps => ps.GetProductsCountByCategory("CategoryName"))
                .Returns(3);

            var count = _productController.GetCount("CategoryName");

            var result = count.Result as ObjectResult;
            var value = result!.Value;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            value.Should().Be(3);
        }

        [Fact]
        public void GetCount_ReturnOk_WhenProductsAreEmpty()
        {
            _productService
                .Setup(ps => ps.GetProductsCountByCategory("CategoryName"))
                .Returns(0);

            var count = _productController.GetCount("CategoryName");

            var result = count.Result as ObjectResult;
            var value = result!.Value;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            value.Should().Be(0);
        }
    }
}