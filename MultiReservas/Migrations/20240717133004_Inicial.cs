using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiReservas.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeLocais = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    QuantidadeLocais = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservasPorLocal = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reservas = table.Column<bool>(type: "INTEGER", nullable: false),
                    Itens = table.Column<bool>(type: "INTEGER", nullable: false),
                    Usuarios = table.Column<bool>(type: "INTEGER", nullable: false),
                    Configuracao = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaginaInicial = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdicionarReservas = table.Column<bool>(type: "INTEGER", nullable: false),
                    EditarReservas = table.Column<bool>(type: "INTEGER", nullable: false),
                    FinalizarReservas = table.Column<bool>(type: "INTEGER", nullable: false),
                    CancelarReservas = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdicionarItensReserva = table.Column<bool>(type: "INTEGER", nullable: false),
                    RemoverItensReserva = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Local = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaItens_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaItens_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configuracoes",
                columns: new[] { "Id", "NomeLocais", "QuantidadeLocais", "ReservasPorLocal" },
                values: new object[] { 1, null, 50, null });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "AdicionarItensReserva", "AdicionarReservas", "Ativo", "CancelarReservas", "Configuracao", "EditarReservas", "FinalizarReservas", "Itens", "Login", "PaginaInicial", "RemoverItensReserva", "Reservas", "Senha", "Usuarios" },
                values: new object[] { 1, true, true, true, true, true, true, true, true, "admin", true, true, true, "7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40", true });

            migrationBuilder.CreateIndex(
                name: "IX_ReservaItens_ItemId",
                table: "ReservaItens",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaItens_ReservaId",
                table: "ReservaItens",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracoes");

            migrationBuilder.DropTable(
                name: "ReservaItens");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
