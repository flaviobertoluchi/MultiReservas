using MultiReservas.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(500, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [DisplayName("Preço")]
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
