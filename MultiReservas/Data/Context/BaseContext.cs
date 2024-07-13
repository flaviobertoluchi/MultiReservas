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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().Property(x => x.Preco).HasPrecision(18, 2);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Login = "admin",
                    Senha = "7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40",
                    Ativo = true
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
