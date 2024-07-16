using MultiReservas.Config;
using System.ComponentModel;
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

        // Administração

        public bool Reservas { get; set; }
        public bool Itens { get; set; }

        [DisplayName("Usuários")]
        public bool Usuarios { get; set; }

        [DisplayName("Configuração")]
        public bool Configuracao { get; set; }

        // Operação

        [DisplayName("Página Inicial")]
        public bool PaginaInicial { get; set; }

        [DisplayName("Adicionar Reservas")]
        public bool AdicionarReservas { get; set; }

        [DisplayName("Editar Reservas")]
        public bool EditarReservas { get; set; }

        [DisplayName("Finalizar Reservas")]
        public bool FinalizarReservas { get; set; }

        [DisplayName("Cancelar Reservas")]
        public bool CancelarReservas { get; set; }

        [DisplayName("Adicionar Itens a Reserva")]
        public bool AdicionarItensReserva { get; set; }

        [DisplayName("Remover Itens da Reserva")]
        public bool RemoverItensReserva { get; set; }

    }
}
