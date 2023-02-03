using AutoMapper;
using Bogus;
using FluentAssertions;
using Marketplace.Core.Entities;
using Marketplace.WebAPI.DTO;
using Marketplace.WebAPI.Mapping;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarketplaceWebAPI.Tests
{
    public class MappingTests
    {
        private readonly Mapper _mapper;

        public MappingTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())));
        }

        [Fact]
        public void Map_ReturnCorrectMainCategoryDto_WhenThereAreSpacesAndUpperCase()
        {
            var mainCategory = new MainCategory
            {
                Name = "MainCategory name",
                SubCategories = new List<Category> { new Category { Name = "Category Long Name" } }
            };

            var dto = _mapper.Map<MainCategoryDto>(mainCategory);

            dto.Should().NotBeNull()
                .And.BeOfType<MainCategoryDto>();
            dto.Route.Should().NotBeNullOrEmpty()
                .And.NotBeUpperCased()
                .And.Be("maincategory-name");
            dto.SubCategories.Should().NotBeNullOrEmpty()
                .And.AllSatisfy(ct =>
                    ct.Route.Should().NotBeNullOrEmpty()
                    .And.NotBeUpperCased()
                    .And.Be("category-long-name")
                    );
        }

        [Fact]
        public void Map_ReturnCorrectProductDto_WhenThereIsSignificantTag()
        {
            var product = new Product
            {
                TagValues = new List<TagValue>
                {
                    new TagValue { Value = "Tag value", Tag = new Tag { Name = "Tag name" } },
                    new TagValue { Value = "Tag value", Tag = new Tag { Name = "Price" } }
                }
            };

            var dto = _mapper.Map<ProductDto>(product);

            dto.Should().NotBeNull()
                .And.BeOfType<ProductDto>();
            dto.TagValues.Should().HaveCount(1)
                .And.AllBeOfType<TagValueDto>();
            dto.TagValues.Should().AllSatisfy(tv => tv.Name.Should().Be("Price"));
        }

        [Fact]
        public void Map_ReturnCorrectProductDtos_WhenThereAreRandomTags()
        {
            Randomizer.Seed = new Random(Guid.NewGuid().GetHashCode());
            var tags = new[] { "Price", "Condition", "Salary", "Type", "Brand", "Material" };

            var testTag = new Faker<Tag>()
                .RuleFor(t => t.Name, f => f.PickRandom(tags));
            var testTagValues = new Faker<TagValue>()
                .RuleFor(tv => tv.Tag, f => testTag.Generate());
            var testProducts = new Faker<Product>()
                .RuleFor(p => p.TagValues, f => testTagValues.Generate(2));
            var products = testProducts.Generate(2);

            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            var significantTags = new[] { "Price", "Condition", "Salary" };
            dtos.Should().NotBeNullOrEmpty()
                .And.AllBeOfType<ProductDto>()
                .And.HaveCount(2)
                .And.AllSatisfy(p =>
                {
                    if(p.TagValues?.Count is not 0)
                        p.TagValues.Should().AllSatisfy(tv =>
                        {
                            tv.Name.Should().NotBeNullOrEmpty()
                            .And.BeOneOf(significantTags);
                        });
                });
        }
    }
}