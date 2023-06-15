using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using crud_mvc.Service;
using System.Linq;
using crud_mvc.Models.Enums;

namespace crud_mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly LoginService _context;

        public HomeController(LoginService context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var obj = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value);
            var email = obj.First();
            var user = await _context.FindByEmail(email);
            return View(user);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
