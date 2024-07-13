using MultiReservas.Models;
using System.Text.Json;

namespace MultiReservas.Extensions
{
    public class Sessao(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        private readonly string usuario = "usuario";

        public Usuario? ObterUsuario()
        {
            var sessao = accessor.HttpContext?.Session.GetString(usuario);
            if (sessao is null) return null;

            return JsonSerializer.Deserialize<Usuario>(sessao, options);
        }

        public void Adicionar(string key, string value)
        {
            accessor.HttpContext?.Session.SetString(key, value);
        }

        public void Excluir(string key)
        {
            accessor.HttpContext?.Session.Remove(key);
        }
    }
}
