using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using crud_mvc.Service;

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
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var email = claims.First().Value;
            var obj = await _context.FindByEmail(email);
            return View(obj);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
