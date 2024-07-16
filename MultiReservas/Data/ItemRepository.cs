using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data
{
    public class ItemRepository<T>(T context) : IItemRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<Paginacao<Item>> ObterPaginado(int pagina, int qtdPorPagina, ItemOrdem ordem, bool desc, string pesquisa, string pesquisaNome, bool desativado)
        {
            var query = context.Itens.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Id.ToString().Contains(pesquisa));
            if (!string.IsNullOrEmpty(pesquisaNome)) query = query.Where(x => x.Nome.Contains(pesquisaNome));
            if (!desativado) query = query.Where(x => x.Ativo);

            if (desc)
            {
                query = ordem switch
                {
                    ItemOrdem.Nome => query.OrderByDescending(x => x.Nome).ThenByDescending(x => x.Id),
                    ItemOrdem.Preco => query.OrderByDescending(x => x.Preco).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    ItemOrdem.Nome => query.OrderBy(x => x.Nome).ThenBy(x => x.Id),
                    ItemOrdem.Preco => query.OrderBy(x => x.Preco).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Item>()
            {
                Data = await query.Skip(qtdPorPagina * (pagina - 1)).Take(qtdPorPagina).ToListAsync(),
                Info = new()
                {
                    TotalItens = totalItens,
                    TotalPaginas = totalPaginas,
                    QtdPorPagina = qtdPorPagina,
                    PaginaAtual = pagina,
                    PaginaAnterior = totalItens > 1 && pagina > 1 ? pagina - 1 : null,
                    PaginaProxima = pagina < totalPaginas ? pagina + 1 : null
                }
            };
        }

        public async Task<ICollection<Item>> ObterTodos()
        {
            return await context.Itens.AsNoTracking().ToListAsync();
        }
        public async Task<Item?> Obter(int id)
        {
            return await context.Itens.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Item item)
        {
            context.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Item item)
        {
            context.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
