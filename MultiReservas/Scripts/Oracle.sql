﻿DECLARE
V_COUNT INTEGER;
BEGIN
SELECT COUNT(TABLE_NAME) INTO V_COUNT from USER_TABLES where TABLE_NAME = '__EFMigrationsHistory';
IF V_COUNT = 0 THEN
Begin
BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"__EFMigrationsHistory" (
    "MigrationId" NVARCHAR2(150) NOT NULL,
    "ProductVersion" NVARCHAR2(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)';
END;

End;

END IF;
EXCEPTION
WHEN OTHERS THEN
    IF(SQLCODE != -942)THEN
        RAISE;
    END IF;
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Configuracoes" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "NomeLocais" NVARCHAR2(100),
    "QuantidadeLocais" NUMBER(10) NOT NULL,
    "ReservasPorLocal" NUMBER(10),
    CONSTRAINT "PK_Configuracoes" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Itens" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "Nome" NVARCHAR2(500) NOT NULL,
    "Preco" DECIMAL(18,2) NOT NULL,
    "Ativo" BOOLEAN NOT NULL,
    CONSTRAINT "PK_Itens" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Usuarios" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "Login" NVARCHAR2(20) NOT NULL,
    "Senha" NVARCHAR2(64) NOT NULL,
    "Ativo" BOOLEAN NOT NULL,
    "Reservas" BOOLEAN NOT NULL,
    "Itens" BOOLEAN NOT NULL,
    "Usuarios" BOOLEAN NOT NULL,
    "Configuracao" BOOLEAN NOT NULL,
    "PaginaInicial" BOOLEAN NOT NULL,
    "AdicionarReservas" BOOLEAN NOT NULL,
    "EditarReservas" BOOLEAN NOT NULL,
    "FinalizarReservas" BOOLEAN NOT NULL,
    "CancelarReservas" BOOLEAN NOT NULL,
    "AdicionarItensReserva" BOOLEAN NOT NULL,
    "RemoverItensReserva" BOOLEAN NOT NULL,
    CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"Reservas" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "UsuarioId" NUMBER(10) NOT NULL,
    "Local" NUMBER(10) NOT NULL,
    "Nome" NVARCHAR2(100) NOT NULL,
    "Status" NUMBER(10) NOT NULL,
    "DataInicio" TIMESTAMP(7) NOT NULL,
    "DataFim" TIMESTAMP(7),
    "Observacao" NVARCHAR2(2000),
    CONSTRAINT "PK_Reservas" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Reservas_Usuarios_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"ReservaItens" (
    "Id" NUMBER(10) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "ReservaId" NUMBER(10) NOT NULL,
    "ItemId" NUMBER(10) NOT NULL,
    "Quantidade" NUMBER(10) NOT NULL,
    CONSTRAINT "PK_ReservaItens" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ReservaItens_Itens_ItemId" FOREIGN KEY ("ItemId") REFERENCES "Itens" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ReservaItens_Reservas_ReservaId" FOREIGN KEY ("ReservaId") REFERENCES "Reservas" ("Id") ON DELETE CASCADE
)';
END;
/

BEGIN
INSERT INTO "Configuracoes" ("Id", "NomeLocais", "QuantidadeLocais", "ReservasPorLocal")
VALUES (1, NULL, 50, NULL);
END;
/

BEGIN
INSERT INTO "Usuarios" ("Id", "AdicionarItensReserva", "AdicionarReservas", "Ativo", "CancelarReservas", "Configuracao", "EditarReservas", "FinalizarReservas", "Itens", "Login", "PaginaInicial", "RemoverItensReserva", "Reservas", "Senha", "Usuarios")
VALUES (1, True, True, True, True, True, True, True, True, N'admin', True, True, True, N'7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40', True);
END;
/

CREATE INDEX "IX_ReservaItens_ItemId" ON "ReservaItens" ("ItemId")
/

CREATE INDEX "IX_ReservaItens_ReservaId" ON "ReservaItens" ("ReservaId")
/

CREATE INDEX "IX_Reservas_UsuarioId" ON "Reservas" ("UsuarioId")
/

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES (N'20240717140405_Inicial', N'8.0.7')
/
