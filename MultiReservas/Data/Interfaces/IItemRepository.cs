using MultiReservas.Models;

namespace MultiReservas.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<ICollection<Item>> ObterTodos();
    }
}
