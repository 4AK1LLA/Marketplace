﻿namespace Marketplace.Core.DTO;

public class CategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Dictionary<int, string>? Tags { get; set; }
}