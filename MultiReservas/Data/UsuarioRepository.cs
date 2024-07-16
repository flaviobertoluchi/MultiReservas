using Microsoft.EntityFrameworkCore;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data
{
    public class UsuarioRepository<T>(T context) : IUsuarioRepository where T : BaseContext
    {
        private readonly T context = context;

        public async Task<Paginacao<Usuario>> ObterPaginado(int pagina, int qtdPorPagina, UsuarioOrdem ordem, bool desc, string pesquisa, string pesquisaLogin, bool desativado)
        {
            var query = context.Usuarios.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Id.ToString().Contains(pesquisa));
            if (!string.IsNullOrEmpty(pesquisaLogin)) query = query.Where(x => x.Login.Contains(pesquisaLogin));
            if (!desativado) query = query.Where(x => x.Ativo);

            if (desc)
            {
                query = ordem switch
                {
                    UsuarioOrdem.Login => query.OrderByDescending(x => x.Login).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    UsuarioOrdem.Login => query.OrderBy(x => x.Login).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Usuario>()
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

        public async Task<Usuario?> ObterPorLoginESenha(string login, string senha)
        {
            return await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login && x.Senha == senha && x.Ativo);
        }

        public async Task<Usuario?> Obter(int id)
        {
            return await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Usuario usuario)
        {
            context.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            context.Update(usuario);
            await context.SaveChangesAsync();
        }
    }
}
