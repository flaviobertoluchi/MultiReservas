using MultiReservas.Models;

namespace MultiReservas.Data.Interfaces
{
    public interface IConfiguracaoRepository
    {
        Task<Configuracao?> Obter();
        Task Atualizar(Configuracao configuracao);
    }
}
