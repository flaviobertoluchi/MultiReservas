using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;

namespace MultiReservas.Data
{
    public class ConfiguracaoRepository<T>(T context) : IConfiguracaoRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<Configuracao?> Obter()
        {
            return await context.Configuracoes.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Atualizar(Configuracao configuracao)
        {
            context.Update(configuracao);
            await context.SaveChangesAsync();
        }
    }
}
