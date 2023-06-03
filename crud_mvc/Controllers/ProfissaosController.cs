using crud_mvc.Models;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
