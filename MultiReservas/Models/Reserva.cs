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

        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        public ReservaStatus Status { get; set; }

        [DisplayName("Data")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Data Fim")]
        public DateTime? DataFim { get; set; }

        public ICollection<ReservaItem> ReservaItens { get; set; } = [];

        public Usuario? Usuario { get; set; }
    }
}
