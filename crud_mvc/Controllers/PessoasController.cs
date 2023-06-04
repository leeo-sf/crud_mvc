using crud_mvc.Models;
using crud_mvc.Models.ViewModels;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            obj.Pessoa.GeraIdade(obj.Pessoa.DataNascimento);
            await _pessoaService.Create(obj.Pessoa);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _pessoaService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
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
