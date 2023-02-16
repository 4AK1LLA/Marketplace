using System.ComponentModel.DataAnnotations;

namespace Marketplace.IdentityServer.ViewModels;

public class LoginViewModel
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? ReturnUrl { get; set; }
}
