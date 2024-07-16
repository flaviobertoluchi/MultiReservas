using Microsoft.AspNetCore.Mvc;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;
using System.Diagnostics;

namespace MultiReservas.Controllers
{
    [UsuarioAutorizacao]
    public class HomeController(IReservaRepository reservaRepository, IConfiguracaoRepository configuracaoRepository) : Controller
    {
        private readonly IReservaRepository reservaRepository = reservaRepository;
        private readonly IConfiguracaoRepository configuracaoRepository = configuracaoRepository;

        public async Task<IActionResult> Index()
        {
            var configuracao = await configuracaoRepository.Obter();
            ViewBag.NomeLocais = configuracao?.NomeLocais;
            ViewBag.QuantidadeLocais = configuracao?.QuantidadeLocais;
            return View(await reservaRepository.ObterTodos(ReservaStatus.Aberta));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
