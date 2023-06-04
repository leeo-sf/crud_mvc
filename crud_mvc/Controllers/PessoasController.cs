using crud_mvc.Models;
using crud_mvc.Models.ViewModels;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace crud_mvc.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoaService _pessoaService;
        private readonly ProfissaoService _profissaoService;

        public PessoasController(PessoaService pessoaService, ProfissaoService profissaoService)
        {
            _pessoaService = pessoaService;
            _profissaoService = profissaoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDePessoas = await _pessoaService.ToList();
            return View(listaDePessoas);
        }

        public async Task<IActionResult> Create()
        {
            var listaDeProfissao = await _profissaoService.ToList();
            var viewModel = new PessoaFormView { Profissao = listaDeProfissao };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PessoaFormView obj)
        {
            await _pessoaService.Create(obj.Pessoa);
            return RedirectToAction(nameof(Index));
        }
    }
}
