using Marketplace.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.IdentityServer.Controllers;

public class AuthController : Controller
{
    private SignInManager<IdentityUser> _signInManager;
    private UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
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

        var user = new IdentityUser(vm.Email);
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
                ModelState.AddModelError("ConfirmPassword", "User with this email exists");
            }
            else
            {
                ModelState.AddModelError(
                "ConfirmPassword", "Password must have at least 5 characters and include digit, uppercase, lowercase"
                );
            }
        }

        return View(vm);
    }
}