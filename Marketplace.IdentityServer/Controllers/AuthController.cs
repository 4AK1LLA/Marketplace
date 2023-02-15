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
    public async Task<IActionResult> Login(LoginViewModel vm)
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

        return View(vm);
    }

    [HttpGet]
    public IActionResult Register([FromQuery] string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = new IdentityUser(vm.Email);
        var result = await _userManager.CreateAsync(user, vm.Password);

        if(result.Succeeded)
        {
            await _signInManager
                .PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false);

            return Redirect(vm.ReturnUrl!);
        }

        return View(vm);
    }
}