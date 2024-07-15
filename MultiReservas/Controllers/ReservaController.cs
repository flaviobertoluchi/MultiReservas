using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Controllers
{
    [UsuarioAutorizacao]
    [Route("reserva")]
    public class ReservaController(IReservaRepository repository, IItemRepository itemRepository, Sessao sessao) : Controller
    {
        private readonly IReservaRepository repository = repository;
        private readonly IItemRepository itemRepository = itemRepository;
        private readonly Sessao sessao = sessao;

        [Route("local")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("local/{local}")]
        public async Task<IActionResult> Local(int local)
        {
            ViewBag.Local = local;
            var reservas = await repository.ObterTodos(ReservaStatus.Aberta, local);

            if (reservas is null || reservas.Count <= 0) return RedirectToAction(nameof(Adicionar), new { local });
            if (reservas.Count == 1) return RedirectToAction(nameof(Detalhes), new { id = reservas.FirstOrDefault()?.Id });

            return View(reservas);
        }

        [Route("adicionar")]
        public IActionResult Adicionar(int local)
        {
            return View(new Reserva() { Local = local, DataInicio = DateTime.Today.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute) });
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(Reserva model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = sessao.ObterUsuario();
            if (usuario is null) return View(model);

            model.UsuarioId = usuario.Id;
            model.Status = ReservaStatus.Aberta;
            await repository.Adicionar(model);

            return RedirectToAction(nameof(Detalhes), new { id = model.Id });
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            ViewBag.Itens = (await itemRepository.ObterTodos()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).ToList();

            return View(await repository.Obter(id));
        }

        [HttpPost("detalhes/{id}")]
        public async Task<IActionResult> Detalhes(int id, Reserva model)
        {
            ViewBag.Itens = (await itemRepository.ObterTodos()).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            }).ToList();

            if (!ModelState.IsValid || id != model.Id) return View(model);

            var reserva = await repository.Obter(id);
            if (reserva is null) return View(model);

            reserva.Local = model.Local;
            reserva.Nome = model.Nome;
            reserva.Status = model.Status;
            reserva.DataInicio = model.DataInicio;
            reserva.DataFim = model.DataFim;
            reserva.ReservaItens = model.ReservaItens;

            await repository.Atualizar(reserva);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost("reservaitem")]
        public async Task<IActionResult> ReservaItemPartial(ReservaItem reservaItem)
        {
            if (!ModelState.IsValid
                || reservaItem.ReservaId <= 0
                || reservaItem.ItemId <= 0
                || reservaItem.Quantidade <= 0) return PartialView("_ReservaItemPartial");

            var reserva = await repository.Obter(reservaItem.ReservaId, true);
            if (reserva is null) return PartialView("_ReservaItemPartial");

            var reservaItemBanco = reserva.ReservaItens.FirstOrDefault(x => x.ItemId == reservaItem.ItemId);
            if (reservaItemBanco is null)
                reserva.ReservaItens.Add(reservaItem);
            else
                reservaItemBanco.Quantidade += reservaItem.Quantidade;

            await repository.Atualizar(reserva);

            return PartialView("_ReservaItemPartial", (await repository.Obter(reserva.Id))?.ReservaItens);
        }
    }
}
