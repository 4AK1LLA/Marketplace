using System.ComponentModel.DataAnnotations;

namespace Marketplace.IdentityServer.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }

    public string? ReturnUrl { get; set; }
}