using Ubicaciones.Aplicacion.DTOs;
using Ubicaciones.Dominio.Entidades;
using Ubicaciones.Dominio.Repositorios;

namespace Ubicaciones.Aplicacion.Servicios;

public class ContactoServicio : IContactoServicio
{
    private readonly IContactoRepositorio _repositorio;

    public ContactoServicio(IContactoRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<ContactoRespuesta>> ObtenerTodosAsync(string? buscar)
    {
        var contactos = await _repositorio.ObtenerTodosAsync(buscar);
        return contactos.Select(ARespuesta).ToList();
    }

    public async Task<ContactoRespuesta?> ObtenerPorIdAsync(int id)
    {
        var contacto = await _repositorio.ObtenerPorIdAsync(id);
        return contacto is null ? null : ARespuesta(contacto);
    }

    public async Task<ContactoRespuesta> CrearAsync(ContactoSolicitud solicitud)
    {
        var contacto = AEntidad(solicitud, new Contacto());
        var creado = await _repositorio.CrearAsync(contacto);
        return ARespuesta(creado);
    }

    public async Task<bool> ActualizarAsync(int id, ContactoSolicitud solicitud)
    {
        var existente = await _repositorio.ObtenerPorIdAsync(id);
        if (existente is null)
        {
            return false;
        }

        AEntidad(solicitud, existente);
        return await _repositorio.ActualizarAsync(existente);
    }

    public Task<bool> EliminarAsync(int id)
    {
        return _repositorio.EliminarAsync(id);
    }

    private static Contacto AEntidad(ContactoSolicitud solicitud, Contacto contacto)
    {
        contacto.Nombre = solicitud.Nombre.Trim();
        contacto.Telefono = solicitud.Telefono.Trim();
        contacto.Latitud = solicitud.Latitud!.Value;
        contacto.Longitud = solicitud.Longitud!.Value;
        contacto.VideoRuta = solicitud.VideoRuta;
        contacto.Imagen = string.IsNullOrWhiteSpace(solicitud.ImagenBase64)
            ? null
            : Convert.FromBase64String(solicitud.ImagenBase64);
        return contacto;
    }

    private static ContactoRespuesta ARespuesta(Contacto contacto)
    {
        return new ContactoRespuesta
        {
            Id = contacto.Id,
            Nombre = contacto.Nombre,
            Telefono = contacto.Telefono,
            Latitud = contacto.Latitud,
            Longitud = contacto.Longitud,
            VideoRuta = contacto.VideoRuta,
            ImagenBase64 = contacto.Imagen is null ? null : Convert.ToBase64String(contacto.Imagen)
        };
    }
}
