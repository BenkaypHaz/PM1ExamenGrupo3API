namespace Ubicaciones.Aplicacion.DTOs;

public class ContactoRespuesta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public string? ImagenBase64 { get; set; }

    public string? VideoRuta { get; set; }
}
