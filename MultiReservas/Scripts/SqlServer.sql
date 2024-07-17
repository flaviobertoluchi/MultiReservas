IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Configuracoes] (
    [Id] int NOT NULL IDENTITY,
    [NomeLocais] nvarchar(100) NULL,
    [QuantidadeLocais] int NOT NULL,
    [ReservasPorLocal] int NULL,
    CONSTRAINT [PK_Configuracoes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Itens] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(500) NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Itens] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Login] nvarchar(20) NOT NULL,
    [Senha] nvarchar(64) NOT NULL,
    [Ativo] bit NOT NULL,
    [Reservas] bit NOT NULL,
    [Itens] bit NOT NULL,
    [Usuarios] bit NOT NULL,
    [Configuracao] bit NOT NULL,
    [PaginaInicial] bit NOT NULL,
    [AdicionarReservas] bit NOT NULL,
    [EditarReservas] bit NOT NULL,
    [FinalizarReservas] bit NOT NULL,
    [CancelarReservas] bit NOT NULL,
    [AdicionarItensReserva] bit NOT NULL,
    [RemoverItensReserva] bit NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Reservas] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Local] int NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [Status] int NOT NULL,
    [DataInicio] datetime2 NOT NULL,
    [DataFim] datetime2 NULL,
    [Observacao] nvarchar(2000) NULL,
    CONSTRAINT [PK_Reservas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reservas_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ReservaItens] (
    [Id] int NOT NULL IDENTITY,
    [ReservaId] int NOT NULL,
    [ItemId] int NOT NULL,
    [Quantidade] int NOT NULL,
    CONSTRAINT [PK_ReservaItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ReservaItens_Itens_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Itens] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReservaItens_Reservas_ReservaId] FOREIGN KEY ([ReservaId]) REFERENCES [Reservas] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NomeLocais', N'QuantidadeLocais', N'ReservasPorLocal') AND [object_id] = OBJECT_ID(N'[Configuracoes]'))
    SET IDENTITY_INSERT [Configuracoes] ON;
INSERT INTO [Configuracoes] ([Id], [NomeLocais], [QuantidadeLocais], [ReservasPorLocal])
VALUES (1, NULL, 50, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'NomeLocais', N'QuantidadeLocais', N'ReservasPorLocal') AND [object_id] = OBJECT_ID(N'[Configuracoes]'))
    SET IDENTITY_INSERT [Configuracoes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdicionarItensReserva', N'AdicionarReservas', N'Ativo', N'CancelarReservas', N'Configuracao', N'EditarReservas', N'FinalizarReservas', N'Itens', N'Login', N'PaginaInicial', N'RemoverItensReserva', N'Reservas', N'Senha', N'Usuarios') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [AdicionarItensReserva], [AdicionarReservas], [Ativo], [CancelarReservas], [Configuracao], [EditarReservas], [FinalizarReservas], [Itens], [Login], [PaginaInicial], [RemoverItensReserva], [Reservas], [Senha], [Usuarios])
VALUES (1, CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'admin', CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), N'7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdicionarItensReserva', N'AdicionarReservas', N'Ativo', N'CancelarReservas', N'Configuracao', N'EditarReservas', N'FinalizarReservas', N'Itens', N'Login', N'PaginaInicial', N'RemoverItensReserva', N'Reservas', N'Senha', N'Usuarios') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;
GO

CREATE INDEX [IX_ReservaItens_ItemId] ON [ReservaItens] ([ItemId]);
GO

CREATE INDEX [IX_ReservaItens_ReservaId] ON [ReservaItens] ([ReservaId]);
GO

CREATE INDEX [IX_Reservas_UsuarioId] ON [Reservas] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240717134047_Inicial', N'8.0.7');
GO

COMMIT;
GO

