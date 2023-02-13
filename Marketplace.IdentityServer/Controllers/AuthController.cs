using Marketplace.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.IdentityServer.Controllers;

public class AuthController : Controller
{
    private SignInManager<IdentityUser> _signInManager;

    public AuthController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
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

        var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false); //FIX: failed with valid credentials

        if (result.Succeeded)
        {
            return Redirect(vm.ReturnUrl!);
        }

        return View(vm.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel vm)
    {
        return Redirect(vm.ReturnUrl!);
    }
}