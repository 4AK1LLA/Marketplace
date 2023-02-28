using IdentityServer4.Services;
using Marketplace.IdentityServer.Interfaces;
using Marketplace.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.IdentityServer.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailAddressValidator _emailAddressValidator;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(
        SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager, 
        IEmailAddressValidator emailAddressValidator,
        IIdentityServerInteractionService interactionService
        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailAddressValidator = emailAddressValidator;
        _interactionService = interactionService;
    }

    [HttpGet]
    public IActionResult Login([FromQuery] string returnUrl)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var result = await _signInManager
            .PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Redirect(vm.ReturnUrl!);
        }

        ModelState.AddModelError("Password", "Email or Password is incorrect");
        return View(vm);
    }

    [HttpGet]
    public IActionResult Register([FromQuery] string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (!_emailAddressValidator.IsValid(vm.Email!))
        {
            ModelState.AddModelError("Email", "Email does not match required pattern");
        }

        var user = new IdentityUser(vm.Email)
        {
            Email = vm.Email
        };
        var result = await _userManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            await _signInManager
                .PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false);

            return Redirect(vm.ReturnUrl!);
        }

        if (result.Errors.Any())
        {
            if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
            {
                ModelState.AddModelError("Email", "User with this email exists");
            }
            else
            {
                ModelState.AddModelError(
                "Password", "Password must have at least 5 characters and include digit, uppercase, lowercase"
                );
            }
        }

        return View(vm);
    }

    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        if (string.IsNullOrEmpty(provider))
        {
            return RedirectToAction(nameof(Login), "Auth", new { returnUrl });
        }

        var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);

        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info is null)
        {
            return RedirectToAction(nameof(Login), "Auth", new { returnUrl });
        }

        var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = string.Empty;

        if (info.LoginProvider == "Facebook")
        {
            userName = "facebook-" + identifier;
        }

        if (info.LoginProvider == "Google")
        {
            userName = "google-" + identifier;
        }

        if (string.IsNullOrEmpty(userName))
        {
            return RedirectToAction(nameof(Login), "Auth", new { returnUrl });
        }

        var foundUser = await _userManager.FindByNameAsync(userName);

        if (foundUser is not null)
        {
            await _signInManager.SignInAsync(foundUser, isPersistent: false);

            return Redirect(returnUrl);
        }

        var user = new IdentityUser(userName);

        var emailClaim = info.Principal.FindFirst(ClaimTypes.Email);

        if (emailClaim is not null)
        {
            user.Email = emailClaim.Value;
        }

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return RedirectToAction(nameof(Login), "Auth", new { returnUrl });
        }

        var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);

        if (!string.IsNullOrEmpty(fullName))
        {
            await _userManager.AddClaimAsync(user, new Claim("display_name", fullName));
        }

        var profilePictureClaim = info.Principal.FindFirst("image");

        if (profilePictureClaim is not null)
        {
            await _userManager.AddClaimAsync(user, new Claim("profile_picture", profilePictureClaim.Value));
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return Redirect(returnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();

        var logoutContext = await _interactionService.GetLogoutContextAsync(logoutId);

        return Redirect(logoutContext.PostLogoutRedirectUri);
    }
}