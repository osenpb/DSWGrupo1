using System.ComponentModel.DataAnnotations;

namespace DSWGrupo01.Models
{
    public class UsuarioModel
    {
        public int Id_Usuario { get; set; }

        public int Id_Rol { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico invalido")]
        [StringLength(100, ErrorMessage = "El correo no debe superar los 100 caracteres")]
        [Display(Name = "Correo")]
        public String Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=(?:.*\d){3,}).{6,}$", ErrorMessage = "La contraseña debe tener mínimo 6 caracteres y al menos 3 números.")]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "Dni obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Ingrese los 8 dígitos de su DNI")]
        public String Dni {  get; set; }

        [Required(ErrorMessage = "Dirección obligatoria")]
        [StringLength(150, ErrorMessage = "La dirección no debe superar los 150 caracteres")]
        [Display(Name = "Dirección")]
        public String Direccion {  get; set; }

        [Required(ErrorMessage = "Teléfono obligatorio")]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "El numero no debe superar los 9 caracteres")]
        [Display(Name = "Teléfono")]
        public String Telefono { get; set; }

        public DateTime Fecha_Registro { get; set; } = DateTime.Now;
    }
}
