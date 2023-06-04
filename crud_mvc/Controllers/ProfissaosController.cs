using crud_mvc.Models;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using crud_mvc.Service.Exceptions;
using crud_mvc.Models.ViewModels;
using System.Diagnostics;

namespace crud_mvc.Controllers
{
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
            return View(listaDeProfissoes);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profissao obj)
        {
            await _profissaoService.Create(obj);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _profissaoService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Edit(int id, Profissao obj)
        {
            if (id != obj.Id)
            {
                return BadRequest();
            }
            // testar edit
            try
            {
                await _profissaoService.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
