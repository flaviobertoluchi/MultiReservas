using Microsoft.AspNetCore.Mvc;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;
using System.Text.Json;

namespace MultiReservas.Controllers
{
    [Route("usuario")]
    public class UsuarioController(IUsuarioRepository repository, Sessao sessao) : Controller
    {
        private readonly IUsuarioRepository repository = repository;
        private readonly Sessao sessao = sessao;
        private readonly Usuario usuario = sessao.ObterUsuario() ?? new();
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        [UsuarioAutorizacao]
        public async Task<IActionResult> Index()
        {
            if (!usuario.Usuarios) return View();
            return View(await repository.ObterPaginado(1, 10, UsuarioOrdem.Id, false, "", "", false));
        }

        [UsuarioAutorizacao]
        [Route("paginacao")]
        public async Task<IActionResult> UsuarioPaginacao(int pagina = 1, int qtdPorPagina = 10, UsuarioOrdem ordem = UsuarioOrdem.Id, bool desc = false, string pesquisa = "", string pesquisaLogin = "", bool desativado = false)
        {
            if (!usuario.Usuarios) return PartialView("_UsuarioPaginacaoPartial");
            return PartialView("_UsuarioPaginacaoPartial", await repository.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaLogin, desativado));
        }

        [UsuarioAutorizacao]
        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [UsuarioAutorizacao]
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(Usuario model)
        {
            if (!ModelState.IsValid || !usuario.Usuarios) return View(model);

            model.Senha = CriptografiaSHA256.Criptografar(model.Senha);
            await repository.Adicionar(model);

            return RedirectToAction(nameof(Index));
        }

        [UsuarioAutorizacao]
        [Route("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!usuario.Usuarios) return View();
            return View(await repository.Obter(id));
        }

        [UsuarioAutorizacao]
        [HttpPost("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id, Usuario model)
        {
            if (!ModelState.IsValid || id != model.Id || !usuario.Usuarios) return View(model);

            var usuarioBanco = await repository.Obter(id);
            if (usuarioBanco is null) return View(model);

            usuarioBanco.Login = model.Login;
            usuarioBanco.Senha = CriptografiaSHA256.Criptografar(model.Senha);
            usuarioBanco.Ativo = model.Ativo;
            usuarioBanco.PaginaInicial = model.PaginaInicial;
            usuarioBanco.Reservas = model.Reservas;
            usuarioBanco.Itens = model.Itens;
            usuarioBanco.Usuarios = model.Usuarios;
            usuarioBanco.Configuracao = model.Configuracao;
            usuarioBanco.AdicionarReservas = model.AdicionarReservas;
            usuarioBanco.EditarReservas = model.EditarReservas;
            usuarioBanco.FinalizarReservas = model.FinalizarReservas;
            usuarioBanco.CancelarReservas = model.CancelarReservas;
            usuarioBanco.AdicionarItensReserva = model.AdicionarItensReserva;
            usuarioBanco.RemoverItensReserva = model.RemoverItensReserva;

            await repository.Atualizar(usuarioBanco);

            return RedirectToAction(nameof(Index));
        }

        [Route("entrar")]
        public IActionResult Entrar(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Entrar(string login, string senha, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var usuarioBanco = await repository.ObterPorLoginESenha(login, CriptografiaSHA256.Criptografar(senha));
            if (usuarioBanco is null)
            {
                ViewBag.Mensagem = "Credenciais inválidas.";
                return View();
            }

            sessao.Adicionar("usuario", JsonSerializer.Serialize(usuarioBanco, options));

            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Route("sair")]
        public IActionResult Sair()
        {
            sessao.Excluir("usuario");
            return RedirectToAction(nameof(Entrar));
        }
    }
}
