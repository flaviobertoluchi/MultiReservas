using MultiReservas.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Configuracao
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Nome dos locais")]
        public string? NomeLocais { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [Range(1, 9999, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Range))]
        [DisplayName("Quantidade de locais")]
        public int QuantidadeLocais { get; set; }

        [DisplayName("Reservas por local")]
        public int? ReservasPorLocal { get; set; }
    }
}
