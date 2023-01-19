using Marketplace.Core.Entities;
using Marketplace.Core.Interfaces;
using Marketplace.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _uow;

    public CategoryController(IUnitOfWork uow) => _uow = uow;

    [HttpGet]
    public IEnumerable<CategoryDto> GetAllCategories()
    {
        #region SEED_CATEGORY
        if (_uow.CategoryRepository.Count() == 0)
        {
            var category = new Category
            {
                Name = "Tables",
                Tags = new List<Tag>
                {
                    new Tag { Name = "Size" },
                    new Tag { Name = "Material" }
                }
            };

            _uow.CategoryRepository.Add(category);
            _uow.Save();
        }
        #endregion

        var categories = _uow.CategoryRepository.GetAllIncludingTags();
        var result = new List<CategoryDto>();

        foreach (var ct in categories)
        {
            var dto = new CategoryDto
            {
                Id = ct.Id,
                Name = ct.Name,
                Tags = new Dictionary<int, string>()
            };

            if (ct.Tags != null)
                foreach (var ctTag in ct.Tags)
                {
                    dto.Tags.Add(ctTag.Id, ctTag.Name!);
                }

            result.Add(dto);
        }
        return result;
    }
}