﻿using Microsoft.AspNetCore.Mvc;
using MultiReservas.Config;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Controllers
{
    [UsuarioAutorizacao]
    [Route("configuracao")]
    public class ConfiguracaoController(IConfiguracaoRepository repository, IReservaRepository reservaRepository, Sessao sessao) : Controller
    {
        private readonly IConfiguracaoRepository repository = repository;
        private readonly IReservaRepository reservaRepository = reservaRepository;
        private readonly Usuario usuario = sessao.ObterUsuario() ?? new();

        public async Task<IActionResult> Index()
        {
            if (!usuario.Configuracao) return View();
            return View(await repository.Obter());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Configuracao model)
        {
            if (!ModelState.IsValid || !usuario.Configuracao) return View(model);

            var reservas = await reservaRepository.ObterTodos(ReservaStatus.Aberta);

            var configuracao = await repository.Obter();
            if (configuracao is null) return View(model);


            if (reservas.Any(x => x.Local > model.QuantidadeLocais))
            {
                ViewBag.Mensagem = "Existe uma reserva aberta com local maior que o número escolhido.";
                return View(model);
            }

            if (reservas
                .GroupBy(x => x.Local)
                .Select(x =>
                new
                {
                    local = x.First().Local,
                    quantidade = x.Count()
                })
                .Any(x => x.quantidade > model.ReservasPorLocal))
            {
                ViewBag.Mensagem = "Existe um local com mais reservas que a quantidade escolhida.";
                return View(model);
            }

            configuracao.NomeLocais = model.NomeLocais;
            configuracao.QuantidadeLocais = model.QuantidadeLocais;
            configuracao.ReservasPorLocal = model.ReservasPorLocal;

            await repository.Atualizar(configuracao);

            TempData["Sucesso"] = Mensagens.AtualizarSucesso;

            return View(configuracao);
        }
    }
}
