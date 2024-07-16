using MultiReservas.Config;
using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(64, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
