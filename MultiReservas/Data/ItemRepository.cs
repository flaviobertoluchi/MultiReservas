using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;

namespace MultiReservas.Data
{
    public class ItemRepository<T>(T context) : IItemRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<ICollection<Item>> ObterTodos()
        {
            return await context.Itens.AsNoTracking().ToListAsync();
        }
    }
}
