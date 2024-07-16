using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<Paginacao<Item>> ObterPaginado(int pagina, int qtdPorPagina, ItemOrdem ordem, bool desc, string pesquisa, string pesquisaNome, bool desativado);
        Task<ICollection<Item>> ObterTodos();
        Task<Item?> Obter(int id);
        Task Adicionar(Item item);
        Task Atualizar(Item item);
    }
}
