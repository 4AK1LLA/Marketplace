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
        private const int pagingValidCount = 20;
        private const int pagingValidPage = 1;
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
        public void GetProductsByCategoryAndPage_ReturnAllProductsOfThisCategory_WhenCategoryWithProperNameFound()
        {
            var category = new Category { Name = "CategoryName" };

            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValuesAndPhotos(category.Name, pagingValidPage))
                .Returns(new List<Product> {
                    new Product { Category = category },
                    new Product { Category = category },
                    new Product { Category = category }
                });
            _productRepository
                .Setup(pr => pr.CountByCategoryName(category.Name))
                .Returns(pagingValidCount);

            var products = _productService.GetProductsByCategoryAndPage("CategoryName", pagingValidPage);

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
        public void GetProductsByCategoryAndPage_ReturnEmptyProducts_WhenCategoryWithProperNameNotFound()
        {
            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValuesAndPhotos("CategoryName", pagingValidPage))
                .Returns(Enumerable.Empty<Product>());
            _productRepository
                .Setup(pr => pr.CountByCategoryName("CategoryName"))
                .Returns(pagingValidCount);

            var products = _productService.GetProductsByCategoryAndPage("CategoryName", pagingValidPage);

            products.Should().BeEmpty()
                .And.NotBeNull()
                .And.BeOfType<List<Product>>();
        }

        [Fact]
        public void GetProductsByCategoryAndPage_ReturnProductsWithTags_WhenCategoryWithProperNameFound()
        {
            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValuesAndPhotos("CategoryName", pagingValidPage))
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
            _productRepository
                .Setup(pr => pr.CountByCategoryName("CategoryName"))
                .Returns(pagingValidCount);

            var products = _productService.GetProductsByCategoryAndPage("CategoryName", pagingValidPage);

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

        [Fact]
        public void GetProductsByCategoryAndPage_ReturnNull_WhenPageIsNotValid()
        {
            var page = 100;

            _productRepository
                .Setup(pr => pr.CountByCategoryName("CategoryName"))
                .Returns(16);

            var products = _productService.GetProductsByCategoryAndPage("CategoryName", page);

            products.Should().BeNull();
        }

        [Fact]
        public void GetProductsByCategoryAndPage_ReturnProductsWithCountInRange1And16_WhenCategoryAndPageIsValid()
        {
            _productRepository
                .Setup(pr => pr.GetByCategoryNameIncludingTagValuesAndPhotos("CategoryName", pagingValidPage))
                .Returns(new List<Product> { new Product { Category = new Category() } });
            _productRepository
                .Setup(pr => pr.CountByCategoryName("CategoryName"))
                .Returns(pagingValidCount);

            var products = _productService.GetProductsByCategoryAndPage("CategoryName", pagingValidPage);

            products.Should().NotBeNullOrEmpty();
            products.Count().Should().BeInRange(minimumValue: 1, maximumValue: 16);
        }
    }
}