# MultiReservas

Um sistema constru�do em .NET 8 para reserva de locais, alguns exemplos de uso s�o: restaurantes, hot�is, cinemas e estacionamentos. Pode ser utilizado tamb�m como um sistema de pedidos simples.

![P�gina Inicial](img/pagina_inicial.png)

O sistema tamb�m � responsivo ao tamanho da tela e tema claro/escuro do dispositivo.

![Dispositivo M�vel Tema Claro](img/movel_claro.png)

A quantidade de locais pode ser alterada nas configura��es do sistema, assim como o limite de reservas por local, permitindo multiplas reservas, �til para restaurantes com mais de uma comanda ou reservas futuras. 

![MultiReservas](img/multireservas.png)

## Como executar

### Pr�-requisito

- [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
 
### Instru��es

- Baixe o sistema e descompacte: [MultiReservas.zip](https://github.com/flaviobertoluchi/MultiReservas/blob/master/MultiReservas.zip)
- Execute o sistema abrindo o arquivo MultiReservas.exe ou MultiReservas.dll dependendo do sistema operacional.
- Abra o link gerado no navegador, utilize o usu�rio **admin** com senha **admin**.

#### Banco de dados (opcional)

- O sistema pode ser executado em um dos seguintes bancos:

  - [SQLite](https://www.sqlite.org/download.html) (Padr�o, j� configurado)
  - [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
  - [Oracle Database](https://www.oracle.com/br/database/free/get-started/)
  - [PostgreSQL](https://www.postgresql.org/download/)
  - [MySQL](https://dev.mysql.com/downloads/)

- Escolha um provedor de banco de dados e crie o banco, execute o script de cria��o das tabelas correspondente encontrado em [Scripts](https://github.com/flaviobertoluchi/MultiReservas/tree/master/MultiReservas/Scripts)
- Defina o banco na tag "Banco" do arquivo [appsettings.json](https://github.com/flaviobertoluchi/MultiReservas/blob/master/MultiReservas/appsettings.json), o arquivo cont�m exemplos de ConnectionStrings para cada banco, configure a do banco escolhido.
