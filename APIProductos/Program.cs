using APIProductos.Models.Entities;
using APIProductos.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string audence = "panaderia.itesrc.com";
//La llave deberia ser encriptada
var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("panaderiaLave9.1G"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt =>
{

    jwt.Authority = "https://itesrc.net";
    jwt.Audience = audence;


    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        
        IssuerSigningKey = llave,
        ValidIssuer ="https://itesrc.net",
        ValidAudience = audence,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
    };

});


builder.Services.AddControllers();
builder.Services.AddDbContext<ItesrcneIntegracionContext>(optionsBuilder =>

optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_integracion;user=itesrcne_integra;password=integra1"
, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));

builder.Services.AddTransient<CategoriasRepository>();
builder.Services.AddTransient<Productos2>();
builder.Services.AddTransient<ProductosRepository>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseAuthorization();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();