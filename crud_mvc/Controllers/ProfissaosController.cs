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
    }
}
