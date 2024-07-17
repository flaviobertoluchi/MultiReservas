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

builder.Services.AddControllersWithViews();

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
