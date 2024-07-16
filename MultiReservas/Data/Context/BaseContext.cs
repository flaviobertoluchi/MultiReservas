using Microsoft.EntityFrameworkCore;
using MultiReservas.Models;

namespace MultiReservas.Data.Context
{
    public abstract class BaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ReservaItem> ReservaItens { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().Property(x => x.Preco).HasPrecision(18, 2);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Login = "admin",
                    Senha = "7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40",
                    Ativo = true,
                    PaginaInicial = true,
                    Reservas = true,
                    Itens = true,
                    Usuarios = true,
                    Configuracao = true,
                    AdicionarReservas = true,
                    EditarReservas = true,
                    FinalizarReservas = true,
                    CancelarReservas = true,
                    AdicionarItensReserva = true,
                    RemoverItensReserva = true
                });

            modelBuilder.Entity<Configuracao>().HasData(
                new Configuracao
                {
                    Id = 1,
                    QuantidadeLocais = 100
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
