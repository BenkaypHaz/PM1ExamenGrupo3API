using Ubicaciones.Aplicacion.DTOs;

namespace Ubicaciones.Aplicacion.Servicios;

public interface IContactoServicio
{
    Task<List<ContactoRespuesta>> ObtenerTodosAsync(string? buscar);

    Task<ContactoRespuesta?> ObtenerPorIdAsync(int id);

    Task<ContactoRespuesta> CrearAsync(ContactoSolicitud solicitud);

    Task<bool> ActualizarAsync(int id, ContactoSolicitud solicitud);

    Task<bool> EliminarAsync(int id);
}
