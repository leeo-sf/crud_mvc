using crud_mvc.Models;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace crud_mvc.Controllers
{
    public class AccessController : Controller
    {
        private readonly LoginService _loginService;

        public AccessController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<IActionResult> Login()
        {
            //verifica se usuário já está logado
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario user)
        {
            var obj = await _loginService.CheckLogin(user);
            if (obj == null)
            {
                ViewData["ValidateMessage"] = "Usuário não encontrado";
                return View();
            }
            List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim("OtherProperties", "Example Role") };

            ClaimsIdentity claimsIdentify = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
                //IsPersistent = user.ManterLogado
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentify), properties);

            return RedirectToAction("Index", "Home");
        }
    }
}
