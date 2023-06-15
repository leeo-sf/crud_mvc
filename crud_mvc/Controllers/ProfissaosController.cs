using crud_mvc.Models;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using crud_mvc.Service.Exceptions;
using crud_mvc.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace crud_mvc.Controllers
{
    //só pode entrar na classe se estiver logado
    [Authorize]
    public class ProfissaosController : Controller
    {
        private readonly ProfissaoService _profissaoService;

        public ProfissaosController(ProfissaoService profissaoService)
        {
            _profissaoService = profissaoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeProfissoes = await _profissaoService.ToList();
            var obj = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            var role = obj.First();
            var permission = false;
            if (role == "1")
            {
                permission = true;
            }
            var viewModel = new PermissionFormView { Profissao = listaDeProfissoes, Permission = permission };
            return View(viewModel);
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create(Profissao obj)
        {
            await _profissaoService.Create(obj);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _profissaoService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _profissaoService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Edit(int id, Profissao obj)
        {
            if (id != obj.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido incompatível com o id do obj" });
            }
            
            try
            {
                await _profissaoService.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _profissaoService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _profissaoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
