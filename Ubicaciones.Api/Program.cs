using Microsoft.EntityFrameworkCore;
using Ubicaciones.Aplicacion.Servicios;
using Ubicaciones.Dominio.Repositorios;
using Ubicaciones.Infraestructura.Persistencia;
using Ubicaciones.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://0.0.0.0:4445");

var cadenaConexion = builder.Configuration.GetConnectionString("MySql");
builder.Services.AddDbContext<ContextoBd>(opciones =>
    opciones.UseMySql(cadenaConexion, new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped<IContactoRepositorio, ContactoRepositorio>();
builder.Services.AddScoped<IContactoServicio, ContactoServicio>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Ubicaciones API",
        Version = "v1",
        Description = "REST API del examen grupal: CRUD de contactos con ubicación (lat/lng), imagen y video."
    });
});

var app = builder.Build();

using (var alcance = app.Services.CreateScope())
{
    var contexto = alcance.ServiceProvider.GetRequiredService<ContextoBd>();
    contexto.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
