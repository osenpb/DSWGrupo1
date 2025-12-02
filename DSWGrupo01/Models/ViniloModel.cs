using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSWGrupo01.Models
{
    public class ViniloModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "El artista es obligatorio")]
        public string? Artista { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El anio es obligatorio")]
        public DateTime Anio { get; set; }
        
        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El stock es obligatorio")]
        public int Stock { get; set; }
        [Display(Name = "Url de la imagen")]
        [Required(ErrorMessage = "La imagen es obligatoria")]
        public string? ImagenUrl { get; set; } // link de la imagen del vinilo

        [Display(Name = "Vista previa")]
        public string? Preview { get; set; } // link de la cancion

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string? Descripcion { get; set; }

        public string? FechaIngreso { get; set; }


    }
}
