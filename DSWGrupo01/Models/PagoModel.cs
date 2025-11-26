using System.ComponentModel.DataAnnotations;

namespace DSWGrupo01.Models
{
    public class PagoModel
    {
        // Datos del usuario
        public int? Id_Usuario { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "DNI obligatorio")]
        [StringLength(8, MinimumLength = 8)]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Correo obligatorio")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Teléfono obligatorio")]
        [StringLength(9, MinimumLength = 7)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Dirección obligatoria")]
        [StringLength(150)]
        public string Direccion { get; set; }

        // Crear cuenta con datos ingresados?
        public bool CrearCuenta { get; set; }

        // Contraseña solo si CrearCuenta = true
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=(?:.*\d){3,}).{6,}$",
            ErrorMessage = "La contraseña debe tener mínimo 6 caracteres y al menos 3 números.")]
        public string? Contrasenia { get; set; }

        // Dirección de envío distinta?
        public bool UsarDireccionDiferente { get; set; }

        [StringLength(150)]
        public string? Direccion_Envio { get; set; }

        // Carrito
        public int? Id_Carrito { get; set; }

        public List<CarritoProducto> Items { get; set; } = new List<CarritoProducto>();

        public decimal Total { get; set; }

        // Pago
        [Required(ErrorMessage = "Seleccione un método de pago")]
        public string Metodo_Pago { get; set; }
    }
}
