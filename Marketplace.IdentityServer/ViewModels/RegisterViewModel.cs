using System.ComponentModel.DataAnnotations;

namespace Marketplace.IdentityServer.ViewModels;

public class RegisterViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }

    public string? ReturnUrl { get; set; }
}