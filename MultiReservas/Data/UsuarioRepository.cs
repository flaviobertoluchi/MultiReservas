using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;

namespace MultiReservas.Data
{
    public class UsuarioRepository<T>(T context) : IUsuarioRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<Usuario?> ObterPorLoginESenha(string login, string senha)
        {
            return await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login && x.Senha == senha && x.Ativo);
        }
    }
}
