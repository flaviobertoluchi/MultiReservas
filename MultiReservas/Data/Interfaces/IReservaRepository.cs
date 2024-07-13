using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data.Interfaces
{
    public interface IReservaRepository
    {
        Task<ICollection<Reserva>> ObterTodos(ReservaStatus? status, int? local);
        Task<Reserva?> Obter(int id);
        Task Adicionar(Reserva reserva);
        Task Atualizar(Reserva reserva);
    }
}
