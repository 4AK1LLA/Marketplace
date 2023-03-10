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
    }
}