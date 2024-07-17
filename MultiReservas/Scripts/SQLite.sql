CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Configuracoes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Configuracoes" PRIMARY KEY AUTOINCREMENT,
    "NomeLocais" TEXT NULL,
    "QuantidadeLocais" INTEGER NOT NULL,
    "ReservasPorLocal" INTEGER NULL
);

CREATE TABLE "Itens" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Itens" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NOT NULL,
    "Preco" TEXT NOT NULL,
    "Ativo" INTEGER NOT NULL
);

CREATE TABLE "Usuarios" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Usuarios" PRIMARY KEY AUTOINCREMENT,
    "Login" TEXT NOT NULL,
    "Senha" TEXT NOT NULL,
    "Ativo" INTEGER NOT NULL,
    "Reservas" INTEGER NOT NULL,
    "Itens" INTEGER NOT NULL,
    "Usuarios" INTEGER NOT NULL,
    "Configuracao" INTEGER NOT NULL,
    "PaginaInicial" INTEGER NOT NULL,
    "AdicionarReservas" INTEGER NOT NULL,
    "EditarReservas" INTEGER NOT NULL,
    "FinalizarReservas" INTEGER NOT NULL,
    "CancelarReservas" INTEGER NOT NULL,
    "AdicionarItensReserva" INTEGER NOT NULL,
    "RemoverItensReserva" INTEGER NOT NULL
);

CREATE TABLE "Reservas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reservas" PRIMARY KEY AUTOINCREMENT,
    "UsuarioId" INTEGER NOT NULL,
    "Local" INTEGER NOT NULL,
    "Nome" TEXT NOT NULL,
    "Status" INTEGER NOT NULL,
    "DataInicio" TEXT NOT NULL,
    "DataFim" TEXT NULL,
    "Observacao" TEXT NULL,
    CONSTRAINT "FK_Reservas_Usuarios_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ReservaItens" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ReservaItens" PRIMARY KEY AUTOINCREMENT,
    "ReservaId" INTEGER NOT NULL,
    "ItemId" INTEGER NOT NULL,
    "Quantidade" INTEGER NOT NULL,
    CONSTRAINT "FK_ReservaItens_Itens_ItemId" FOREIGN KEY ("ItemId") REFERENCES "Itens" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ReservaItens_Reservas_ReservaId" FOREIGN KEY ("ReservaId") REFERENCES "Reservas" ("Id") ON DELETE CASCADE
);

INSERT INTO "Configuracoes" ("Id", "NomeLocais", "QuantidadeLocais", "ReservasPorLocal")
VALUES (1, NULL, 50, NULL);
SELECT changes();


INSERT INTO "Usuarios" ("Id", "AdicionarItensReserva", "AdicionarReservas", "Ativo", "CancelarReservas", "Configuracao", "EditarReservas", "FinalizarReservas", "Itens", "Login", "PaginaInicial", "RemoverItensReserva", "Reservas", "Senha", "Usuarios")
VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 'admin', 1, 1, 1, '7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40', 1);
SELECT changes();


CREATE INDEX "IX_ReservaItens_ItemId" ON "ReservaItens" ("ItemId");

CREATE INDEX "IX_ReservaItens_ReservaId" ON "ReservaItens" ("ReservaId");

CREATE INDEX "IX_Reservas_UsuarioId" ON "Reservas" ("UsuarioId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240717133004_Inicial', '8.0.7');

COMMIT;

