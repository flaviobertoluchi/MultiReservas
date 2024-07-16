using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data.Interfaces
{
    public interface IReservaRepository
    {
        Task<Paginacao<Reserva>> ObterPaginado(int pagina, int qtdPorPagina, ReservaOrdem ordem, bool desc, string pesquisa, string pesquisaNome, DateTime? dataInicial, DateTime? dataFinal);
        Task<ICollection<Reserva>> ObterTodos(ReservaStatus? status = null, int? local = null);
        Task<Reserva?> Obter(int id, bool comTrack = false);
        Task Adicionar(Reserva reserva);
        Task Atualizar(Reserva reserva);
    }
}
