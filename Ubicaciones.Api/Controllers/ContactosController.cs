using Microsoft.AspNetCore.Mvc;
using Ubicaciones.Aplicacion.DTOs;
using Ubicaciones.Aplicacion.Servicios;

namespace Ubicaciones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactosController : ControllerBase
{
    private readonly IContactoServicio _servicio;

    public ContactosController(IContactoServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContactoRespuesta>>> ObtenerTodos([FromQuery] string? buscar)
    {
        return Ok(await _servicio.ObtenerTodosAsync(buscar));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContactoRespuesta>> ObtenerPorId(int id)
    {
        var contacto = await _servicio.ObtenerPorIdAsync(id);
        if (contacto is null)
        {
            return NotFound(new { mensaje = $"No existe el contacto con id {id}" });
        }

        return Ok(contacto);
    }

    [HttpPost]
    public async Task<ActionResult<ContactoRespuesta>> Crear([FromBody] ContactoSolicitud solicitud)
    {
        try
        {
            var creado = await _servicio.CrearAsync(solicitud);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (FormatException)
        {
            return BadRequest(new { mensaje = "La imagen no tiene un formato Base64 válido" });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ContactoSolicitud solicitud)
    {
        try
        {
            var actualizado = await _servicio.ActualizarAsync(id, solicitud);
            if (!actualizado)
            {
                return NotFound(new { mensaje = $"No existe el contacto con id {id}" });
            }

            return Ok(new { mensaje = "Contacto actualizado" });
        }
        catch (FormatException)
        {
            return BadRequest(new { mensaje = "La imagen no tiene un formato Base64 válido" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _servicio.EliminarAsync(id);
        if (!eliminado)
        {
            return NotFound(new { mensaje = $"No existe el contacto con id {id}" });
        }

        return Ok(new { mensaje = "Contacto eliminado" });
    }
}
