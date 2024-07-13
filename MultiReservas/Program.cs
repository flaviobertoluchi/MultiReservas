using Microsoft.EntityFrameworkCore;
using MultiReservas.Data;
using MultiReservas.Data.Context;
using MultiReservas.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Migrations precisa de um ativo
builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var banco = builder.Configuration.GetRequiredSection("Banco");
switch (banco.Value)
{
    case "SqlServer":
        builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        builder.Services.AddScoped<IReservaRepository, ReservaRepository<SqlServerContext>>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository<SqlServerContext>>();
        break;
    default:
        throw new Exception("Banco inválido");
}

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
