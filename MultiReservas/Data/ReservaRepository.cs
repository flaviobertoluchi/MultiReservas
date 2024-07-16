using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data
{
    public class ReservaRepository<T>(T context) : IReservaRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<Paginacao<Reserva>> ObterPaginado(int pagina, int qtdPorPagina, ReservaOrdem ordem, bool desc, string pesquisa, string pesquisaNome, DateTime? dataInicial, DateTime? dataFinal)
        {
            var query = context.Reservas.AsNoTracking().Include(x => x.Usuario).AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Id.ToString().Contains(pesquisa));
            if (!string.IsNullOrEmpty(pesquisaNome)) query = query.Where(x => x.Nome.Contains(pesquisaNome));
            if (dataInicial is not null) query = query.Where(x => x.DataInicio >= dataInicial);
            if (dataFinal is not null) query = query.Where(x => x.DataInicio < dataFinal);


            if (desc)
            {
                query = ordem switch
                {
                    ReservaOrdem.Data => query.OrderByDescending(x => x.DataInicio).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    ReservaOrdem.Data => query.OrderBy(x => x.DataInicio).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Reserva>()
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

        public async Task<ICollection<Reserva>> ObterTodos(ReservaStatus? status = null, int? local = null)
        {
            var query = context.Reservas.AsNoTracking().AsQueryable();

            if (status is not null) query = query.Where(x => x.Status == status);
            if (local is not null) query = query.Where(x => x.Local == local);

            return await query.ToListAsync();
        }

        public async Task<Reserva?> Obter(int id, bool comTrack = false)
        {
            var query = context.Reservas.AsQueryable();

            if (!comTrack) query = query.AsNoTracking();

            return await query.Include(x => x.Usuario).Include(x => x.ReservaItens).ThenInclude(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Reserva reserva)
        {
            context.Add(reserva);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Reserva reserva)
        {
            context.Update(reserva);
            await context.SaveChangesAsync();
        }
    }
}
