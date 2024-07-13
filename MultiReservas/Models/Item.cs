using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Item
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Nome { get; set; } = string.Empty;

        [DisplayName("Preço")]
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
