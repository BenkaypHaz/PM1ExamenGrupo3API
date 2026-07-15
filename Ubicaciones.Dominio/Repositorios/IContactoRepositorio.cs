using Ubicaciones.Dominio.Entidades;

namespace Ubicaciones.Dominio.Repositorios;


public interface IContactoRepositorio
{
    Task<List<Contacto>> ObtenerTodosAsync(string? buscar);

    Task<Contacto?> ObtenerPorIdAsync(int id);

    Task<Contacto> CrearAsync(Contacto contacto);

    Task<bool> ActualizarAsync(Contacto contacto);

    Task<bool> EliminarAsync(int id);
}
