﻿namespace Marketplace.Core.Entities;

public class AppUser
{
    public int Id { get; set; }

    public string? StsIdentifier { get; set; }

    public string? UserName { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public ICollection<Product>? Products { get; set; }

    public ICollection<Product>? LikedProducts { get; set; }

    //TODO: add roles
}
