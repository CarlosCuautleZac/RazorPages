using APIProductos.Models.Entities;
using APIProductos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ItesrcneIntegracionContext>(optionsBuilder =>

optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_integracion;user=itesrcne_integra;password=integra1"
, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));

builder.Services.AddTransient<CategoriasRepository>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();