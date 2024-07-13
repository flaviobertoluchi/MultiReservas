using MultiReservas.Models;

namespace MultiReservas.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorLoginESenha(string login, string senha);
    }
}
