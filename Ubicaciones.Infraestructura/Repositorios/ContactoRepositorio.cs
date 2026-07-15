using Microsoft.EntityFrameworkCore;
using Ubicaciones.Dominio.Entidades;
using Ubicaciones.Dominio.Repositorios;
using Ubicaciones.Infraestructura.Persistencia;

namespace Ubicaciones.Infraestructura.Repositorios;

public class ContactoRepositorio : IContactoRepositorio
{
    private readonly ContextoBd _contexto;

    public ContactoRepositorio(ContextoBd contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Contacto>> ObtenerTodosAsync(string? buscar)
    {
        var consulta = _contexto.Contactos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var texto = buscar.Trim();
            consulta = consulta.Where(c => c.Nombre.Contains(texto) || c.Telefono.Contains(texto));
        }

        return await consulta.OrderByDescending(c => c.Id).ToListAsync();
    }

    public Task<Contacto?> ObtenerPorIdAsync(int id)
    {
        return _contexto.Contactos.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contacto> CrearAsync(Contacto contacto)
    {
        _contexto.Contactos.Add(contacto);
        await _contexto.SaveChangesAsync();
        return contacto;
    }

    public async Task<bool> ActualizarAsync(Contacto contacto)
    {
        _contexto.Contactos.Update(contacto);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var contacto = await _contexto.Contactos.FindAsync(id);
        if (contacto is null)
        {
            return false;
        }

        _contexto.Contactos.Remove(contacto);
        await _contexto.SaveChangesAsync();
        return true;
    }
}
