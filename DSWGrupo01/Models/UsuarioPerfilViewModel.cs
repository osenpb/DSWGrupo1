using System.ComponentModel.DataAnnotations;

namespace DSWGrupo01.Models
{
    public class UsuarioPerfilViewModel
    {
        public int Id_Usuario { get; set; }

        [Display(Name = "Rol")]
        public int Id_Rol { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico invalido")]
        [StringLength(100, ErrorMessage = "El correo no debe superar los 100 caracteres")]
        [Display(Name = "Correo")]
        public string Email { get; set; } // solo lectura

        [Required(ErrorMessage = "Dni obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Ingrese los 8 dígitos de su DNI")]
        public string Dni { get; set; }   // solo lectura

        [Required(ErrorMessage = "Dirección obligatoria")]
        [StringLength(150, ErrorMessage = "La dirección no debe superar los 150 caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Teléfono obligatorio")]
        [RegularExpression(@"^(\d{7}|\d{9})$", ErrorMessage = "El teléfono debe tener exactamente 7 o 9 números")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=(?:.*\d){3,}).{6,}$", ErrorMessage = "La contraseña debe tener mínimo 6 caracteres y al menos 3 números.")]
        [Display(Name = "Nueva Contraseña")]
        public string? NuevaContrasenia { get; set; }
    }
}
