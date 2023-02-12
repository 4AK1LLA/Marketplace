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
        var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Redirect(vm.ReturnUrl!);
        }

        return View();
    }
}