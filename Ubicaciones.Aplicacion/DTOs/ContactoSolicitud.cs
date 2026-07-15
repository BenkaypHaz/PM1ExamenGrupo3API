using System.ComponentModel.DataAnnotations;

namespace Ubicaciones.Aplicacion.DTOs;


public class ContactoSolicitud
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[\p{L} ]+$", ErrorMessage = "El nombre solo admite letras y espacios")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "El teléfono debe tener 8 dígitos")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "La latitud es obligatoria")]
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
    public double? Latitud { get; set; }

    [Required(ErrorMessage = "La longitud es obligatoria")]
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
    public double? Longitud { get; set; }

    public string? ImagenBase64 { get; set; }

    public string? VideoRuta { get; set; }
}
