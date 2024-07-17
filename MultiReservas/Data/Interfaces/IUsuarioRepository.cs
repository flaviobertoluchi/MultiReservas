using MultiReservas.Models;
using MultiReservas.Models.Tipos;

namespace MultiReservas.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Paginacao<Usuario>> ObterPaginado(int pagina, int qtdPorPagina, UsuarioOrdem ordem, bool desc, string pesquisa, string pesquisaLogin, bool desativado);
        Task<Usuario?> ObterPorLoginESenha(string login, string senha);
        Task<Usuario?> Obter(int id);
        Task<Usuario?> ObterPorLogin(string login);
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
    }
}
