using FluentAssertions;
using Marketplace.Core.Interfaces;
using Moq;
using Xunit;

namespace Marketplace.Services.Tests
{
    public class SeederTests
    {
        private readonly Seeder _seeder;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMainCategoryRepository> _mainCategoryRepository;
        private readonly Mock<IProductRepository> _productRepository;

        public SeederTests()
        {
            _mockUow = new Mock<IUnitOfWork>();

            _mainCategoryRepository = new Mock<IMainCategoryRepository>();
            _mockUow.SetupGet(uow => uow.MainCategoryRepository).Returns(_mainCategoryRepository.Object);

            _productRepository = new Mock<IProductRepository>();
            _mockUow.SetupGet(uow => uow.ProductRepository).Returns(_productRepository.Object);

            _seeder = new Seeder(_mockUow.Object);
        }

        [Fact]
        public void Seed_DoesNotStart_WhenThereAreMainCategoriesOrProducts()
        {
            _mainCategoryRepository
                .Setup(mr => mr.Count())
                .Returns(0);

            _productRepository
                .Setup(pr => pr.Count())
                .Returns(10);

            var result = _seeder.Seed();

            result.Should().Be(false);
        }
    }
}