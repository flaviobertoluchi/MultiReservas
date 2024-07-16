﻿using Microsoft.AspNetCore.Mvc;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Controllers
{
    [UsuarioAutorizacao]
    [Route("item")]
    public class ItemController(IItemRepository repository) : Controller
    {
        private readonly IItemRepository repository = repository;

        public async Task<IActionResult> Index()
        {
            return View(await repository.ObterPaginado(1, 10, ItemOrdem.Id, false, "", "", false));
        }

        [Route("paginacao")]
        public async Task<IActionResult> ItemPaginacao(int pagina = 1, int qtdPorPagina = 10, ItemOrdem ordem = ItemOrdem.Id, bool desc = false, string pesquisa = "", string pesquisaNome = "", bool desativado = false)
        {
            return PartialView("_ItemPaginacaoPartial", await repository.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaNome, desativado));
        }

        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(Item model)
        {
            if (!ModelState.IsValid) return View(model);

            await repository.Adicionar(model);

            return RedirectToAction(nameof(Index));
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            return View(await repository.Obter(id));
        }

        [HttpPost("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id, Item model)
        {
            if (!ModelState.IsValid || id != model.Id) return View(model);

            var item = await repository.Obter(id);
            if (item is null) return View(model);

            item.Nome = model.Nome;
            item.Preco = model.Preco;
            item.Ativo = model.Ativo;

            await repository.Atualizar(model);

            return RedirectToAction(nameof(Index));
        }
    }
}