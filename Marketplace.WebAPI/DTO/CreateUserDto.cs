namespace Marketplace.WebAPI.DTO;

public class CreateUserDto
{
    public string? StsIdentifier { get; set; }

    public string? UserName { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? ProfilePhotoUrl { get; set; }
}
