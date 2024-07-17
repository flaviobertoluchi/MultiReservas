﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace MultiReservas.Migrations.MySQL
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NomeLocais = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    QuantidadeLocais = table.Column<int>(type: "int", nullable: false),
                    ReservasPorLocal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracoes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Reservas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Itens = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Usuarios = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Configuracao = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PaginaInicial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AdicionarReservas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EditarReservas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FinalizarReservas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CancelarReservas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AdicionarItensReserva = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RemoverItensReserva = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Local = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Observacao = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReservaItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
