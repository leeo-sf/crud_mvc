using crud_mvc.Models;
using crud_mvc.Models.ViewModels;
using crud_mvc.Service;
using crud_mvc.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace crud_mvc.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoaService _pessoaService;
        private readonly ProfissaoService _profissaoService;
        private readonly GeneroService _generoService;
        private readonly EstadoService _estadoService;

        public PessoasController(PessoaService pessoaService, ProfissaoService profissaoService, GeneroService generoService, EstadoService estadoService)
        {
            _pessoaService = pessoaService;
            _profissaoService = profissaoService;
            _generoService = generoService;
            _estadoService = estadoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDePessoas = await _pessoaService.ToList();
            return View(listaDePessoas);
        }

        public async Task<IActionResult> Create()
        {
            var listaDeEstados = await _estadoService.ToList();
            var listaDeProfissao = await _profissaoService.ToList();
            var listaDeGenero = await _generoService.ToList();
            var viewModel = new PessoaFormView { Profissao = listaDeProfissao, Genero = listaDeGenero, Estado = listaDeEstados };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PessoaFormView obj)
        {
            if (obj.Pessoa.ValidaCPF())
            {
                return RedirectToAction(nameof(Error), new { message = "CPF inválido. Digite somente os números do CPF válido!" });
            }

            if (obj.Pessoa.ProfissaoId == 0)
            {
                return RedirectToAction(nameof(Error), new { message = "Necessário informar uma profissão" });
            }

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

        public async Task<IActionResult> Edit(int? id)
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
            var listaDeEstados = await _estadoService.ToList();
            var listaProfissoes = await _profissaoService.ToList();
            var listaDeGenero = await _generoService.ToList();
            PessoaFormView viewModel = new PessoaFormView { Pessoa = obj, Profissao = listaProfissoes, Genero = listaDeGenero, Estado = listaDeEstados };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PessoaFormView obj)
        {
            if (id != obj.Pessoa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id fornecido incompatível com o id do obj" });
            }

            if (obj.Pessoa.ValidaCPF())
            {
                return RedirectToAction(nameof(Error), new { message = "CPF inválido. Digite somente os números do CPF válido!" });
            }

            try
            {
                obj.Pessoa.GeraIdade(obj.Pessoa.DataNascimento);
                await _pessoaService.Edit(obj.Pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pessoaService.Delete(id);
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
