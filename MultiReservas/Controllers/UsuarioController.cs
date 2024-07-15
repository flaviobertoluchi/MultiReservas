using Microsoft.AspNetCore.Mvc;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using System.Text.Json;

namespace MultiReservas.Controllers
{
    [Route("login")]
    public class UsuarioController(IUsuarioRepository repository, Sessao sessao) : Controller
    {
        private readonly IUsuarioRepository repository = repository;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        private readonly string usuario = "usuario";

        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string login, string senha, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var usuarioBanco = await repository.ObterPorLoginESenha(login, CriptografiaSHA256.Criptografar(senha));
            if (usuarioBanco is null)
            {
                ViewBag.Mensagem = "Credenciais inválidas.";
                return View();
            }

            sessao.Adicionar(usuario, JsonSerializer.Serialize(usuarioBanco, options));

            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Route("sair")]
        public IActionResult Sair()
        {
            sessao.Excluir(usuario);
            return RedirectToAction(nameof(Index));
        }
    }
}
