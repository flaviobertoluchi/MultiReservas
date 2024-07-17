CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Configuracoes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `NomeLocais` varchar(100) NULL,
    `QuantidadeLocais` int NOT NULL,
    `ReservasPorLocal` int NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Itens` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(500) NOT NULL,
    `Preco` decimal(18,2) NOT NULL,
    `Ativo` tinyint(1) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Usuarios` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Login` varchar(20) NOT NULL,
    `Senha` varchar(64) NOT NULL,
    `Ativo` tinyint(1) NOT NULL,
    `Reservas` tinyint(1) NOT NULL,
    `Itens` tinyint(1) NOT NULL,
    `Usuarios` tinyint(1) NOT NULL,
    `Configuracao` tinyint(1) NOT NULL,
    `PaginaInicial` tinyint(1) NOT NULL,
    `AdicionarReservas` tinyint(1) NOT NULL,
    `EditarReservas` tinyint(1) NOT NULL,
    `FinalizarReservas` tinyint(1) NOT NULL,
    `CancelarReservas` tinyint(1) NOT NULL,
    `AdicionarItensReserva` tinyint(1) NOT NULL,
    `RemoverItensReserva` tinyint(1) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Reservas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UsuarioId` int NOT NULL,
    `Local` int NOT NULL,
    `Nome` varchar(100) NOT NULL,
    `Status` int NOT NULL,
    `DataInicio` datetime(6) NOT NULL,
    `DataFim` datetime(6) NULL,
    `Observacao` varchar(2000) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Reservas_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ReservaItens` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ReservaId` int NOT NULL,
    `ItemId` int NOT NULL,
    `Quantidade` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ReservaItens_Itens_ItemId` FOREIGN KEY (`ItemId`) REFERENCES `Itens` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ReservaItens_Reservas_ReservaId` FOREIGN KEY (`ReservaId`) REFERENCES `Reservas` (`Id`) ON DELETE CASCADE
);

INSERT INTO `Configuracoes` (`Id`, `NomeLocais`, `QuantidadeLocais`, `ReservasPorLocal`)
VALUES (1, NULL, 50, NULL);

INSERT INTO `Usuarios` (`Id`, `AdicionarItensReserva`, `AdicionarReservas`, `Ativo`, `CancelarReservas`, `Configuracao`, `EditarReservas`, `FinalizarReservas`, `Itens`, `Login`, `PaginaInicial`, `RemoverItensReserva`, `Reservas`, `Senha`, `Usuarios`)
VALUES (1, TRUE, TRUE, TRUE, TRUE, TRUE, TRUE, TRUE, TRUE, 'admin', TRUE, TRUE, TRUE, '7a8b097ac97ab751667457209d1f714d7473283c000dee9e3cd2c59fa6977b40', TRUE);

CREATE INDEX `IX_ReservaItens_ItemId` ON `ReservaItens` (`ItemId`);

CREATE INDEX `IX_ReservaItens_ReservaId` ON `ReservaItens` (`ReservaId`);

CREATE INDEX `IX_Reservas_UsuarioId` ON `Reservas` (`UsuarioId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240717144758_Inicial', '8.0.7');

COMMIT;

