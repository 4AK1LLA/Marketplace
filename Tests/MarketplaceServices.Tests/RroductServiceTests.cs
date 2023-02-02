using FluentAssertions;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class RroductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _productRepository;

        public RroductServiceTests()
        {
            _mockUow = new Mock<IUnitOfWork>();

            _productRepository = new Mock<IProductRepository>();
            _mockUow.SetupGet(uow => uow.ProductRepository).Returns(_productRepository.Object);

            _productService = new ProductService(_mockUow.Object);
        }

        [Fact]
        public void GetProductsByCategory_ReturnAllProductsOfThisCategory_WhenCategoryWithProperNameFound()
        {
            var category = new Category { Name = "CategoryName" };

            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValues(category.Name))
                .Returns(new List<Product> {
                    new Product { Category = category },
                    new Product { Category = category },
                    new Product { Category = category }
                });

            var products = _productService.GetProductsByCategory("CategoryName");

            products.Should().NotBeNullOrEmpty()
                .And.HaveCount(3)
                .And.AllBeOfType<Product>()
                .And.AllSatisfy(pr =>
                {
                    pr.Category.Should().NotBeNull();
                    pr.Category!.Name.Should().Be("CategoryName");
                });
        }

        [Fact]
        public void GetProductsByCategory_ReturnEmptyProducts_WhenCategoryWithProperNameNotFound()
        {
            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValues("CategoryName"))
                .Returns(Enumerable.Empty<Product>());

            var products = _productService.GetProductsByCategory("CategoryName");

            products.Should().BeEmpty()
                .And.NotBeNull()
                .And.BeOfType<List<Product>>();
        }

        [Fact]
        public void GetProductsByCategory_ReturnProductsWithTags_WhenCategoryWithProperNameFound()
        {
            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValues("CategoryName"))
                .Returns(new List<Product> {
                    new Product {
                        TagValues = new List<TagValue> {
                            new TagValue { Tag = new Tag { Name = "TagName" }, Value = "TagValue" },
                            new TagValue { Tag = new Tag { Name = "TagName" }, Value = "TagValue" }
                        }
                    },
                    new Product {
                        TagValues = new List<TagValue> {
                            new TagValue { Tag = new Tag { Name = "TagName" }, Value = "TagValue" },
                            new TagValue { Tag = new Tag { Name = "TagName" }, Value = "TagValue" }
                        }
                    }
                });

            var products = _productService.GetProductsByCategory("CategoryName");

            products.Should().NotBeNullOrEmpty()
                .And.AllBeOfType<Product>()
                .And.AllSatisfy(pr =>
                {
                    pr.TagValues.Should().NotBeNullOrEmpty()
                        .And.AllBeOfType<TagValue>()
                        .And.AllSatisfy(tv =>
                            tv.Tag.Should().NotBeNull()
                                .And.BeOfType<Tag>());
                });
        }
    }
}