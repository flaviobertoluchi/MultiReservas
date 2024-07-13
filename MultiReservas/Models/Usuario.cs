using System.ComponentModel.DataAnnotations;

namespace MultiReservas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Login { get; set; } = string.Empty;

        [StringLength(64)]
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
