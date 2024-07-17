using Microsoft.AspNetCore.Mvc;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;
using System.Diagnostics;

namespace MultiReservas.Controllers
{
    public class HomeController(IReservaRepository reservaRepository, IConfiguracaoRepository configuracaoRepository, Sessao sessao) : Controller
    {
        private readonly IReservaRepository reservaRepository = reservaRepository;
        private readonly IConfiguracaoRepository configuracaoRepository = configuracaoRepository;
        private readonly Usuario usuario = sessao.ObterUsuario() ?? new();

        [UsuarioAutorizacao]
        public async Task<IActionResult> Index()
        {
            if (!(usuario.Reservas || usuario.PaginaInicial)) return View();

            var configuracao = await configuracaoRepository.Obter();
            ViewBag.NomeLocais = configuracao?.NomeLocais;
            ViewBag.QuantidadeLocais = configuracao?.QuantidadeLocais;
            return View(await reservaRepository.ObterTodos(ReservaStatus.Aberta));
        }

        [UsuarioAutorizacao]
        [Route("indexpartial")]
        public async Task<IActionResult> IndexPartial()
        {
            if (!(usuario.Reservas || usuario.PaginaInicial)) PartialView("_IndexPartial");

            var configuracao = await configuracaoRepository.Obter();
            ViewBag.NomeLocais = configuracao?.NomeLocais;
            ViewBag.QuantidadeLocais = configuracao?.QuantidadeLocais;
            return PartialView("_IndexPartial", await reservaRepository.ObterTodos(ReservaStatus.Aberta));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
