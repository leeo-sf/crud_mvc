using crud_mvc.Models;
using crud_mvc.Service;
using Microsoft.AspNetCore.Mvc;

namespace crud_mvc.Controllers
{
    public class PessoasController : Controller
    {
        private readonly PessoaService _pessoaService;

        public PessoasController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDePessoas = await _pessoaService.ToList();
            return View(listaDePessoas);
        }
    }
}
