using Marketplace.IdentityServer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.IdentityServer.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Login(LoginDto dto)
    {
        return View();
    }
}