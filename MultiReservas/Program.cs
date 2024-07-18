using Microsoft.EntityFrameworkCore;
using MultiReservas.Data;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;
using MultiReservas.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var banco = builder.Configuration.GetRequiredSection("Banco");
switch (banco.Value)
{
    case "SQLite":
        builder.Services.AddDbContext<SQLiteContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<SQLiteContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<SQLiteContext>>();
        builder.Services.AddScoped<IItemRepository, ItemRepository<SQLiteContext>>();
        builder.Services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository<SQLiteContext>>();
        break;
    case "SqlServer":
        builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<SqlServerContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<SqlServerContext>>();
        builder.Services.AddScoped<IItemRepository, ItemRepository<SqlServerContext>>();
        builder.Services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository<SqlServerContext>>();
        break;
    case "Oracle":
        builder.Services.AddDbContext<OracleContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<OracleContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<OracleContext>>();
        builder.Services.AddScoped<IItemRepository, ItemRepository<OracleContext>>();
        builder.Services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository<OracleContext>>();
        break;
    case "PostgreSQL":
        builder.Services.AddDbContext<PostgreSQLContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<PostgreSQLContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<PostgreSQLContext>>();
        builder.Services.AddScoped<IItemRepository, ItemRepository<PostgreSQLContext>>();
        builder.Services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository<PostgreSQLContext>>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        break;
    case "MySQL":
        builder.Services.AddDbContext<MySQLContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySQL") ?? string.Empty));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<MySQLContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<MySQLContext>>();
        builder.Services.AddScoped<IItemRepository, ItemRepository<MySQLContext>>();
        builder.Services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository<MySQLContext>>();
        break;
    default:
        throw new Exception("Banco inválido");
}

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Sessao>();

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchido.");
});

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var ptBR = new CultureInfo("pt-BR");
app.UseRequestLocalization(options: new()
{
    DefaultRequestCulture = new(ptBR),
    SupportedCultures = [ptBR],
    SupportedUICultures = [ptBR]
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
