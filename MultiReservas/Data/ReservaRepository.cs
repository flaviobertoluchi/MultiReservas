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

        public async Task<ICollection<Reserva>> ObterTodos(ReservaStatus? status = null, int? local = null)
        {
            var query = context.Reservas.AsNoTracking().AsQueryable();

            if (status is not null) query = query.Where(x => x.Status == status);
            if (local is not null && local > 0) query = query.Where(x => x.Local == local);

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
