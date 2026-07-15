namespace Ubicaciones.Dominio.Entidades;
public class Contacto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public byte[]? Imagen { get; set; }

    public string? VideoRuta { get; set; }
}
