using FluentAssertions;
using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class MainCategoryServiceTests
    {
        private readonly MainCategoryService _mainCategoryService;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMainCategoryRepository> _mainCategoryRepository;

        public MainCategoryServiceTests()
        {
            _mockUow = new Mock<IUnitOfWork>();

            _mainCategoryRepository = new Mock<IMainCategoryRepository>();
            _mockUow.SetupGet(uow => uow.MainCategoryRepository).Returns(_mainCategoryRepository.Object);

            _mainCategoryService = new MainCategoryService(_mockUow.Object);
        }

        [Fact]
        public void GetAllMainCategories_ReturnAllMainCategories_WhenMainCategoriesNotEmpty()
        {
            _mainCategoryRepository
                .Setup(mr => mr.GetAllIncludingSubcategories())
                .Returns(new List<MainCategory> {
                    new MainCategory(), new MainCategory()
                });

            var mainCategories = _mainCategoryService.GetAllMainCategories();

            mainCategories.Should().NotBeNullOrEmpty()
                .And.HaveCount(2)
                .And.AllBeOfType<MainCategory>();
        }

        [Fact]
        public void GetAllMainCategories_ReturnMainCategoriesWithSubcategories_WhenMainCategoriesNotEmpty()
        {
            _mainCategoryRepository
                .Setup(mr => mr.GetAllIncludingSubcategories())
                .Returns(new List<MainCategory> { 
                    new MainCategory { 
                        Name = "MainCategoryName",
                        SubCategories = new List<Category> { 
                            new Category { MainCategory = new MainCategory { Name = "MainCategoryName" } } 
                        } 
                    } 
                });

            var mainCategories = _mainCategoryService.GetAllMainCategories();

            mainCategories.Should().NotBeNullOrEmpty()
                .And.AllBeOfType<MainCategory>()
                .And.AllSatisfy(mc =>
                {
                mc.SubCategories.Should().NotBeNullOrEmpty()
                    .And.AllSatisfy(x => {
                        x.MainCategory.Should().NotBeNull()
                            .And.BeOfType<MainCategory>();
                        x.MainCategory!.Name.Should().Be("MainCategoryName");
                    });
                });
        }
    }
}