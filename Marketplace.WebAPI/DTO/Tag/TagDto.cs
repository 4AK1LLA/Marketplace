using Marketplace.Core.Entities;

namespace Marketplace.WebAPI.DTO;

public class TagDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsRequired { get; set; }

    public string? Type { get; set; }

    public string? Remark { get; set; }

    public IEnumerable<PossibleValue>? PossibleValues { get; set; }
}
