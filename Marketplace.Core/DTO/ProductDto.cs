﻿namespace Marketplace.Core.DTO;

public class ProductDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? Location { get; set; }

    public CategoryDto? Category { get; set; }

    public Dictionary<string, string>? TagNamesAndValues { get; set; }

    public ICollection<PhotoDto>? Photos { get; set; }
}