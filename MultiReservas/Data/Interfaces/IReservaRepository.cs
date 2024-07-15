using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data.Interfaces
{
    public interface IReservaRepository
    {
        Task<ICollection<Reserva>> ObterTodos(ReservaStatus? status = null, int? local = null);
        Task<Reserva?> Obter(int id, bool comTrack = false);
        Task Adicionar(Reserva reserva);
        Task Atualizar(Reserva reserva);
    }
}
