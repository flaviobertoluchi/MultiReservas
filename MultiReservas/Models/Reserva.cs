using MultiReservas.Config;
using MultiReservas.Models.Tipos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int Local { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Nome { get; set; } = string.Empty;
        public ReservaStatus Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [DisplayName("Data")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Data Fim")]
        public DateTime? DataFim { get; set; }

        public ICollection<ReservaItem> ReservaItens { get; set; } = [];

        [MaxLength(2000, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Observação")]
        public string? Observacao { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
